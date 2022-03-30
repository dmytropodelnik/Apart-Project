using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
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
        public async Task<ActionResult<IEnumerable<Suggestion>>> GetSuggestions(int page = -1, int pageSize = -1)
        {
            try
            {
                List<Suggestion> suggestions = new();
                if (page == -1 || pageSize == -1)
                {
                    suggestions = await _context.Suggestions
                        .Include(s => s.User)
                        .Include(s => s.Address)
                        .Include(s => s.ContactDetails)
                        .Include(s => s.ServiceCategory)
                        .Include(s => s.BookingCategory)
                        .ToListAsync();
                }
                else
                {
                    suggestions = await _context.Suggestions
                        .Include(s => s.User)
                        .Include(s => s.Address)
                        .Include(s => s.ContactDetails)
                        .Include(s => s.ServiceCategory)
                        .Include(s => s.BookingCategory)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                if (suggestions is null)
                {
                    return Json(new { code = STATUS_400 });
                }

                return Json(new { code = STATUS_200, suggestions });
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("addsuggestion")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Suggestion suggestion, IFormFileCollection uploads)
        {
            try
            {
                if (suggestion is null)
                {
                    return Json(new { code = STATUS_400 });
                }

                _context.Suggestions.Add(suggestion);
                await _context.SaveChangesAsync();

                return Json(new { code = STATUS_200 });
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
