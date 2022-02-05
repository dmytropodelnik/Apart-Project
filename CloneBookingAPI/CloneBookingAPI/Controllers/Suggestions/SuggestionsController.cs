using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Suggestions
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController : Controller
    {
        private const int STATUS_200 = 200;
        private const int STATUS_400 = 400;

        private readonly ApartProjectDbContext _context;

        public SuggestionsController(ApartProjectDbContext context)
        {
            _context = context;
        }


        [Route("getsuggestions")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingCategory>>> GetSuggestions()
        {
            try
            {
                var categories = await _context.Suggestions.ToListAsync();
                if (categories is null)
                {
                    return Json(new { code = STATUS_400 });
                }

                return Json(new { code = STATUS_200, bookingCategories = categories });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = STATUS_400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = STATUS_400 });
            }
        }

        [Route("addsuggestion")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Suggestion newSuggestion, IFormFileCollection uploads)
        {
            try
            {
                if (newSuggestion is null)
                {
                    return Json(new { code = STATUS_400 });
                }

                Suggestion suggestion = new();
                // suggestion.
                // newSuggestion.Address

                _context.Suggestions.Add(suggestion);
                await _context.SaveChangesAsync();

                return Json(new { code = STATUS_200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = STATUS_400 });
            }
        }
    }
}
