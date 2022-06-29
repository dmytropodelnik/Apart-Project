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
                        .ThenInclude(a => a.Facilities)
                    .Include(s => s.BookingCategory)
                        .ThenInclude(c => c.BookingCategoryType)
                    .Include(s => s.Highlights)
                    .Include(s => s.Languages)
                    .Include(s => s.Images)
                    .Include(s => s.Reviews)
                        .ThenInclude(r => r.Grades)
                     .Select(s => new {
                            s.Id,
                            s.UniqueCode,
                            s.Name,
                            s.Description,
                            s.IsParkingAvailable,
                            BookingCategory = s.BookingCategory.Category,
                            Country = s.Address.Country.Title,
                            City = s.Address.City.Title,
                            Region = s.Address.Region.Title,
                            Address = s.Address.AddressText,
                            StarsRating = new short[s.StarsRating],
                            ReviewsAmount = s.Reviews.Count,
                            Apartments = s.Apartments
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
                                    ImagePath = a.Image.Path,
                                    ImageName = a.Image.Name,
                                    Facilities = a.Facilities
                                        .Select(f => new
                                        {
                                            f.Id,
                                            f.Text,
                                        })
                                }),
                            Reviews = s.Reviews
                                .Select(r => new
                                {
                                    r.Id,
                                    Grades = r.Grades
                                        .Select(g => new
                                        {
                                            g.Value,
                                        }),
                                }),
                            Images = s.Images.Select(i => new { i.Path, i.Name }),
                        })
                    .FirstOrDefaultAsync(s => s.UniqueCode.Equals(code));
                if (suggestion is null)
                {
                    return Json(new { code = STATUS_400 });
                }

                var facilities = await _context.FacilityTypes
                    .Include(t => t.Facilities)
                        .ThenInclude(f => f.SuggestionsFacilities)
                    .Include(t => t.Image)
                    .Select(t => new 
                    {
                        t.Id,
                        t.Type,
                        Facilities = t.Facilities.Where(f => f.SuggestionsFacilities.Where(sf => sf.SuggestionId == suggestion.Id).Any()),
                    })
                    .Where(t => t.Facilities.Any())
                    .ToListAsync();                 

                var rules = await _context.SuggestionRuleTypes
                    .Include(t => t.SuggestionRules)
                        .ThenInclude(f => f.SuggestionsSuggestionRules)
                    .Select(t => new
                    {
                        t.Id,
                        t.Type,
                        Rules = t.SuggestionRules.Where(f => f.SuggestionsSuggestionRules.Where(sf => sf.SuggestionId == suggestion.Id).Any()),
                    })
                    .Where(t => t.Rules.Any())
                    .ToListAsync();

                int reviewsAmount = suggestion.ReviewsAmount;
                int savedAmount = await _context.Favorites
                    .Include(f => f.Suggestions)
                    .Where(f => f.Suggestions
                        .All(s => s.Id == suggestion.Id))
                    .CountAsync();

                double grade = 0;
                if (suggestion.Reviews.Any())
                {
                    grade = suggestion.Reviews.Average(r => r.Grades.Average(g => g.Value));
                }

                return Json(new { 
                    code = STATUS_200, 
                    suggestion,
                    reviewsAmount,
                    savedAmount,
                    grade,
                    facilities = facilities
                        .Select(t => new
                        {
                            t.Id,
                            t.Type,
                            facilities = t.Facilities
                                .Select(f => new
                                {
                                    f.Id,
                                    f.Text,
                                }),
                        }),
                    rules = rules
                        .Select(t => new
                        {
                            t.Id,
                            t.Type,
                            suggestionRules = t.Rules
                                .Select(r => new
                                {
                                    r.Id,
                                    r.Text,
                                }),
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("getusersuggestions")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suggestion>>> GetUserSuggestions(string email, string filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = STATUS_400, message = "Email data is null." });
                }

                List<Suggestion> suggestions;
                if (string.IsNullOrWhiteSpace(filter))
                {
                    suggestions = await _context.Suggestions
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
                }
                else
                {
                    suggestions = await _context.Suggestions
                        .Include(s => s.User)
                        .Include(s => s.Address)
                        .Include(s => s.Address.Country)
                            .ThenInclude(c => c.Image)
                        .Include(s => s.Address.City)
                        .Include(s => s.Address.Region)
                        .Include(s => s.SuggestionStatus)
                        .Include(s => s.Images)
                        .Include(s => s.Messages)
                        .Where(s => s.User.Email.Equals(email)                 && 
                                    (s.Name.Contains(filter)                   ||
                                    s.Progress.ToString().Contains(filter)     ||
                                    s.UniqueCode.Contains(filter)              ||
                                    s.SuggestionStatus.Status.Contains(filter) ||
                                    s.Messages.Count.ToString().Contains(filter)))
                        .ToListAsync();
                }
                if (suggestions is null)
                {
                    return Json(new { code = STATUS_400, message = "Suggestions are not found." });
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
                    inProgressSuggestions = suggestions
                        .Where(s => s.Progress < 100)
                        .Select(s => new
                        {
                            s.Id,
                            s.UniqueCode,
                            s.Name,
                            s.Progress,
                            messagesCount = s.Messages.Count,
                            suggestionStatus = s.SuggestionStatus.Status,
                            country = s.Address is null ? "" : s.Address.Country.Title,
                            city = s.Address is null ? "" : s.Address.City.Title,
                            region = s.Address is null ? "" : s.Address.Region.Title,
                            address = s.Address is null ? "" : s.Address.AddressText,
                            images = s.Images.Select(i => new { i.Path, i.Name }),
                            countryImage = s.Address?.Country.Image,
                        }),
                    activeSuggestions = suggestions
                        .Where(s => s.Progress == 100)
                        .Select(s => new
                        {
                            s.Id,
                            s.UniqueCode,
                            s.Name,
                            s.Progress,
                            s.SuggestionStatus.Status,
                            messagesCount = s.Messages.Count,
                            suggestionStatus = s.SuggestionStatus.Status,
                            country = s.Address is null ? "" : s.Address.Country.Title,
                            city = s.Address is null ? "" : s.Address.City.Title,
                            region = s.Address is null ? "" : s.Address.Region.Title,
                            address = s.Address is null ? "" : s.Address.AddressText,
                            images = s.Images.Select(i => new { i.Path, i.Name }),
                            countryImage = s.Address?.Country.Image,
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
        [TypeFilter(typeof(AccessorySuggestionFilter))]
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
                    return Json(new { code = STATUS_400, message = "Suggestion is not found." });
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
