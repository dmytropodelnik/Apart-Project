using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Database.Configurations.Suggestions
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedTypesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public BedTypesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getbedtypes")]
        [HttpGet]
        public async Task<ActionResult> GetBedPreferences()
        {
            try
            {
                var bedTypes = await _context.BedTypes
                    .Include(t => t.Beds)
                    .ToListAsync();

                return Json(new
                {
                    code = 200,
                    bedTypes,
                });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }
    }
}
