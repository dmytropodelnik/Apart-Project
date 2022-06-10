using CloneBookingAPI.Services.Database;
using CloneBookingAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Suggestions
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : Controller
    {
        private const int STATUS_200 = 200;
        private const int STATUS_400 = 400;

        private readonly ApartProjectDbContext _context;

        public ApartmentsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("filter")]
        [HttpPost]
        public async Task<ActionResult> Filter([FromBody] ApartmentViewModel filters)
        {
            try
            {
                if (filters is null                            ||
                    filters.GuestsAmount < 1                   ||
                    filters.SearchRoomsAmount < 1              ||
                    string.IsNullOrWhiteSpace(filters.DateIn)  ||
                    string.IsNullOrWhiteSpace(filters.DateOut))
                {
                    return Json(new { code = 400, message = "Input data is null." });
                }

                var apartments = await _context.Apartments
                    .Include(a => a.Suggestion)
                    .Include(a => a.BookedPeriods)
                    .Where(a => a.SuggestionId == filters.SuggestionId     && 
                                a.GuestsLimit >= filters.GuestsAmount      &&
                                a.RoomsAmount >= filters.SearchRoomsAmount &&
                                a.BookedPeriods
                                    .All(bp => (bp.DateIn > Convert.ToDateTime(filters.DateIn) &&
                                                   bp.DateOut > Convert.ToDateTime(filters.DateOut)) ||
                                                   bp.DateIn < Convert.ToDateTime(filters.DateIn) &&
                                                   bp.DateOut < Convert.ToDateTime(filters.DateOut)))
                    .ToListAsync();

                return Json(new
                {
                    code = 200,
                    apartments = apartments
                        .Select(a => new
                        {
                            a.Id,
                            a.Name,
                            a.PriceInUSD,
                            a.RoomsAmount,
                            a.IsSmokingAllowed,
                            a.GuestsLimit,
                            a.ApartmentSize,
                            a.BathroomsAmount,
                            a.Description,
                            a.IsSuite,
                            Facilities = a.Facilities
                                .Select(f => new
                                {
                                    f.Id,
                                    f.Text,
                                })
                        }),
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
