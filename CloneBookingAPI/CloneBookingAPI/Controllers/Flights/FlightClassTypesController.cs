using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Flights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<ActionResult<IEnumerable<FlightClassType>>> GetTypes()
        {
            try
            {
                var res = await _context.FlightClassTypes.ToListAsync();

                return Json(new { code = 200, types = res });
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
        }
    }
}
