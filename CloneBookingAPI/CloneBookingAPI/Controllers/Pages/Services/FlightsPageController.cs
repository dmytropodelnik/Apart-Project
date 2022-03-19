using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.POCOs.Search;
using CloneBookingAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsPageController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public FlightsPageController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult> Search([FromBody] SearchViewModel searchObj)
        {
            try
            {
                if (searchObj is null)
                {
                    return Json(new { code = 400 });
                }


                return Json(new
                {
                    code = 200,
                    // resSuggestions,
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

                return Json(new { code = ex.Message });
            }
        }
    }
}
