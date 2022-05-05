using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<SuggestionHighlight>>> GetHighlights(int page = -1, int pageSize = -1)
        {
            try
            {
                List<SuggestionHighlight> highlights = new();
                if (page == -1 || pageSize == -1)
                {
                    highlights = await _context.SuggestionHighlights
                    //.Include(h => h.Suggestion)
                    //.Include(h => h.Room)
                    .Include(h => h.Image)
                    .ToListAsync();
                }
                else
                {
                    highlights = await _context.SuggestionHighlights
                    //.Include(h => h.Suggestion)
                    //.Include(h => h.Room)
                    .Include(h => h.Image)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                }

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
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
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
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
