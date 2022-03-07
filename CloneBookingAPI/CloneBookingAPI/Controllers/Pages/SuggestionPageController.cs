using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.POCOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionPageController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public SuggestionPageController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getfacilities")]
        [HttpGet]
        public async Task<ActionResult> GetFacilities([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var facilityTypes = await _context.FacilityTypes
                    .Include(f => f.Facilities)
                        .ThenInclude(f => f.Image)
                    .Where(f => f.Facilities
                                    .All(f => f.SuggestionId == suggestion.Id))
                    .ToListAsync();

                return Json(new { 
                    code = 200,
                    facilityTypes,
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

        [Route("getrules")]
        [HttpGet]
        public async Task<ActionResult> GetRules([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var rulesTypes = await _context.SuggestionRuleTypes
                    .Include(r => r.SuggestionRules)
                    .Where(r => r.SuggestionRules
                                    .All(r => r.Suggestions
                                                    .All(s => s.Id == suggestion.Id)))
                    .ToListAsync();

                return Json(new
                {
                    code = 200,
                    rulesTypes,
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
