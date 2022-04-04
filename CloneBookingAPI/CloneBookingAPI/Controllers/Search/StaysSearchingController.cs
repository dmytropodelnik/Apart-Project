using CloneBookingAPI.Controllers.Search.Filtering;
using CloneBookingAPI.Controllers.Search.Pagination;
using CloneBookingAPI.Controllers.Search.Sorting;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.POCOs.Search;
using CloneBookingAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Search
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaysSearchingController : Controller
    {
        private readonly ApartProjectDbContext _context;

        private readonly SuggestionsFilter _suggestionsFilter;
        private readonly ISorter _suggestionsSorter;
        private readonly IPaginator _suggestionsPaginator;

        public StaysSearchingController(
            ApartProjectDbContext context,
            SuggestionsFilter suggestionsFilter,
            SuggestionsSorter suggestionsSorter,
            SuggestionsPaginator suggestionsPaginator)
        {
            _context = context;
            _suggestionsFilter = suggestionsFilter;
            _suggestionsSorter = suggestionsSorter;
            _suggestionsPaginator = suggestionsPaginator;
        }

        [Route("search")]
        [HttpPost]
        public async Task<ActionResult> Search([FromBody] SearchViewModel searchObj)
        {
            try
            {
                if (searchObj is null)
                {
                    return Json(new { code = 400 });
                }
                
                string place = searchObj.Place ?? "";

                var suggestions = await _context.Suggestions
                        .Include(s => s.Address)
                        .Include(s => s.StayBookings)
                        .Include(s => s.Apartments)
                        .Where(s => place.Contains(s.Address.AddressText)        ||
                                    place.Contains(s.Address.Country.Title)      ||
                                    place.Contains(s.Address.City.Title)         ||
                                    s.Address.Country.Title.Contains(place)      ||
                                    s.Address.City.Title.Contains(place)         ||
                                    s.Address.AddressText.Contains(place)        &&
                                    s.Apartments
                                        .Any(a => a.BookedDates
                                            .Any(d => (d.DateIn > Convert.ToDateTime(searchObj.DateIn)      &&
                                                       d.DateIn > Convert.ToDateTime(searchObj.DateOut))    ||
                                                       d.DateOut < Convert.ToDateTime(searchObj.DateIn)     &&
                                                       d.DateOut < Convert.ToDateTime(searchObj.DateOut)    && 
                                                       a.RoomsAmount >= searchObj.SearchRoomsAmount         &&
                                                       a.GuestsLimit >= searchObj.GuestsAmount)))
                        .ToListAsync();
                if (suggestions is null)
                {
                    return Json(new { code = 400 });
                }

                int suggestionsAmount = suggestions.Count();

                // PAGINATION
                suggestions = _suggestionsPaginator.SelectItems(suggestions.AsQueryable(), searchObj.Page, searchObj.PageSize)
                    .ToList();
                if (suggestions is null)
                {
                    return Json(new { code = 400 });
                }

                return Json(new
                {
                    code = 200,
                    suggestions,
                    suggestionsAmount,
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

        [Route("filtersearch")]
        [HttpPost]
        public async Task<ActionResult> FilterSearch([FromBody] SearchViewModel filters)
        {
            try
            {
                if (filters is null)
                {
                    return Json(new { code = 400 });
                }

                var suggestions = await _context.Suggestions
                    //.Include(s => s.Address)
                    //.Include(s => s.Reviews)
                    //.Include(s => s.Facilities)
                    //.Include(s => s.Languages)
                    //.Include(s => s.Beds)
                    //    .ThenInclude(b => b.BedType)
                    //.Include(s => s.Highlights)
                    //.Include(s => s.RoomTypes)
                    //    .ThenInclude(t => t.Rooms)
                    //.Include(s => s.User)
                    .Include(s => s.SuggestionReviewGrades)
                    //.Include(s => s.BookingCategory)
                    .ToListAsync();

                // FILTERING
                var resSuggestions = _suggestionsFilter.FilterItems(suggestions.AsQueryable(), filters.Filters);
                if (resSuggestions is null)
                {
                    return Json(new { code = 400 });
                }

                // SORTING
                resSuggestions = _suggestionsSorter.SortItems(resSuggestions, filters.SortOrder);
                if (resSuggestions is null)
                {
                    return Json(new { code = 400 });
                }

                int suggestionsAmount = resSuggestions.Count();

                // PAGINATION
                resSuggestions = _suggestionsPaginator.SelectItems(resSuggestions, filters.Page, filters.PageSize);
                if (resSuggestions is null)
                {
                    return Json(new { code = 400 });
                }

                return Json(new
                {
                    code = 200,
                    suggestions = resSuggestions.ToList(),
                    suggestionsAmount,
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

        [Route("searchbybookingcategory")]
        [HttpGet]
        public async Task<ActionResult> SearchByBookingCategory(string category)
        {
            try
            {
                if (category is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestions = await _context.Suggestions
                    .Include(s => s.BookingCategory)
                    .Where(s => s.BookingCategory.Category == category)
                    .ToListAsync();

                int countSuggestions = resSuggestions.Count;

                return Json(new
                {
                    code = 200,
                    resSuggestions,
                    countSuggestions
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
