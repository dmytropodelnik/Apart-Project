using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Flights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Flights
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightClassTypesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public FlightClassTypesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("gettypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightClassType>>> GetTypes(int page = -1, int pageSize = -1)
        {
            try
            {
                List<FlightClassType> types = new();
                if (page == -1 || pageSize == -1)
                {
                    types = await _context.FlightClassTypes.ToListAsync();
                }
                else
                {
                    types = await _context.FlightClassTypes
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { code = 200, types });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightClassType>>> Search(string type, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type) || page == -1 || pageSize == -1)
                {
                    var res = await _context.FlightClassTypes.ToListAsync();

                    return Json(new { code = 200, types = res });
                }

                var types = await _context.FlightClassTypes
                    .Where(t => t.Type.Contains(type))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Json(new { code = 200, types });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }
    }
}
