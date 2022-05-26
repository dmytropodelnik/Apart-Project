using CloneBookingAPI.Filters;
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

        [Route("getsuggestion")]
        [HttpGet]
        public async Task<ActionResult<Suggestion>> GetSuggestion(string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { code = STATUS_400 });
                }

                var suggestion = await _context.Suggestions
                    .Include(s => s.Address)
                    .Include(s => s.Address.Country)
                    .Include(s => s.Address.City)
                    .Include(s => s.Address.Region)
                    .Include(s => s.Apartments)
                        .ThenInclude(a => a.BookedPeriods)
                    .Include(s => s.Apartments)
                        .ThenInclude(a => a.RoomTypes)
                        .ThenInclude(a => a.Rooms)
                    .Include(s => s.Beds)
                    .Include(s => s.BookingCategory)
                        .ThenInclude(c => c.BookingCategoryType)
                    .Include(s => s.Facilities)
                        .ThenInclude(f => f.FacilityType)
                    .Include(s => s.Highlights)
                    .Include(s => s.Languages)
                    .Include(s => s.Images)
                    .Include(s => s.Reviews)
                        .ThenInclude(r => r.Grades)
                    .Include(s => s.SuggestionRules)
                        .ThenInclude(r => r.SuggestionRuleType)
                    .FirstOrDefaultAsync(s => s.UniqueCode.Equals(code));
                if (suggestion is null)
                {
                    return Json(new { code = STATUS_400 });
                }

                int reviewsAmount = suggestion.Reviews.Count;

                int savedAmount = await _context.Favorites
                    .Include(f => f.Suggestions)
                    .Where(f => f.Suggestions
                        .All(s => s.Id == suggestion.Id))
                    .CountAsync();

                return Json(new { 
                    code = STATUS_200, 
                    suggestion,
                    reviewsAmount,
                    savedAmount,
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("getusersuggestions")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suggestion>>> GetUserSuggestions(string email)
        {
            try
            {
                var suggestions = await _context.Suggestions
                    .Include(s => s.User)
                    .Include(s => s.Address)
                    .Include(s => s.Address.Country)
                        .ThenInclude(c => c.Image)
                    .Include(s => s.Address.City)
                    .Include(s => s.Address.Region)
                    .Include(s => s.SuggestionStatus)
                    .Include(s => s.Images)
                    .Include(s => s.Messages)
                    .Where(s => s.User.Email.Equals(email))
                    .ToListAsync();
                if (suggestions is null)
                {
                    return Json(new { code = STATUS_400 });
                }

                int activeSuggestionsAmount = suggestions
                    .Where(s => s.Progress == 100)
                    .Count();

                int inProgressSuggestionsAmount = suggestions
                    .Where(s => s.Progress != 100)
                    .Count();

                return Json(new
                {
                    code = STATUS_200,
                    suggestions = suggestions
                        .Select(s => new
                        {
                            s.Id,
                            s.UniqueCode,
                            s.Name,
                            s.Progress,
                            messagesCount = s.Messages.Count,
                            suggestionStatus = s.SuggestionStatus.Status,
                            country = s.Address.Country.Title,
                            city = s.Address.City.Title,
                            region = s.Address.Region.Title,
                            address = s.Address.AddressText,
                            images = s.Images.Select(i => new { i.Path, i.Name }),
                            countryImage = s.Address.Country.Image,
                        }),
                    activeSuggestionsAmount,
                    inProgressSuggestionsAmount,
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("deletesuggestion")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSuggestion(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = STATUS_400 });
                }

                var suggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == id);
                if (suggestion is null)
                {
                    return Json(new { code = STATUS_400 });
                }

                _context.Suggestions.Remove(suggestion);
                await _context.SaveChangesAsync();

                return Json(new { code = STATUS_200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
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
