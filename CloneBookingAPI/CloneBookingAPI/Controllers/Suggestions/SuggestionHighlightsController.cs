using CloneBookingAPI.Services.Database;
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
    public class SuggestionHighlightsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public SuggestionHighlightsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("gethighlights")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuggestionHighlight>>> GetHighlights()
        {
            try
            {
                var highlights = await _context.SuggestionHighlights
                    .Include(h => h.Suggestion)
                    .Include(h => h.Room)
                    .Include(h => h.Image)
                    .ToListAsync();

                return Json(new { code = 200, highlights });
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

        [Route("edithighlight")]
        [HttpPut]
        public async Task<IActionResult> EditHighlight([FromBody] SuggestionHighlight highlight)
        {
            try
            {
                if (highlight is null)
                {
                    return Json(new { code = 400 });
                }

                var resHighlight = await _context.SuggestionHighlights.FirstOrDefaultAsync(h => h.Id == highlight.Id);
                if (resHighlight is null)
                {
                    return Json(new { code = 400 });
                }

                _context.SuggestionHighlights.Update(resHighlight);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }
    }
}
