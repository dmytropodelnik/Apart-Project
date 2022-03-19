using CloneBookingAPI.Controllers.Search.Filtering;
using CloneBookingAPI.Controllers.Search.Pagination;
using CloneBookingAPI.Controllers.Search.Sorting;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
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
        private readonly SuggestionsSorter _suggestionsSorter;
        private readonly SuggestionsPaginator _suggestionsPaginator;

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
        [HttpGet]
        public async Task<ActionResult> Search([FromBody] SearchViewModel filters)
        {
            try
            {
                /*
                int pageHelper = searchObj.Page;

                if (searchObj is null || pageHelper < 1)
                {
                    return Json(new { code = 400 });
                }

                if (searchObj.Page == 1)
                {
                    pageHelper = 0;
                }

                string searchCounty = searchObj.Address.Country.Title ?? "";
                string searchCity = searchObj.Address.City.Title ?? "";
                string searchAddressText = searchObj.Address.AddressText ?? "";

                var resSuggestions = await _context.Suggestions
                       .Include(s => s.Address)
                       .Include(s => s.Images)
                       .Include(s => s.BookingCategory)
                       .Include(s => s.StayBookings)
                       .Include(s => s.Reviews)
                       .Include(s => s.Beds)
                       .Include(s => s.RoomTypes)
                       .Include(s => s.ReviewCategories)
                       .Include(s => s.SuggestionReviewGrades)
                       .Where(s => s.Address.Country.Title.Contains(searchCounty)    ||
                                   s.Address.City.Title.Contains(searchCity)         ||
                                   s.Address.AddressText.Contains(searchAddressText) ||
                                   searchAddressText.Contains(s.Address.AddressText) ||
                                   searchCounty.Contains(s.Address.Country.Title)    ||
                                   searchCity.Contains(s.Address.City.Title)         ||
                                   !s.StayBookings
                                            .All(b => (b.CheckIn  >  Convert.ToDateTime(searchObj.DateIn)    &&
                                                       b.CheckIn  >  Convert.ToDateTime(searchObj.DateOut))  ||
                                                      (b.CheckOut <  Convert.ToDateTime(searchObj.DateIn)    &&
                                                       b.CheckOut <  Convert.ToDateTime(searchObj.DateOut))
                                                       ) &&
                                   s.GuestsAmount >= searchObj.GuestsAmount &&
                                   s.RoomsAmount >= searchObj.RoomsAmount)
                       .ToListAsync();

                List<Suggestion> filteredSuggestions = new();

                if (searchObj.Filter.ToLower().Equals("byLowestPrice".ToLower()))
                {
                    filteredSuggestions = OrderByLowestPrice(resSuggestions);
                }
                else if (searchObj.Filter.ToLower().Equals("byHighestPrice".ToLower()))
                {
                    filteredSuggestions = OrderByHighestPrice(resSuggestions);
                }
                else if (searchObj.Filter.ToLower().Equals("byHomesAndApartmentsFirst".ToLower()))
                {
                    filteredSuggestions = FilterByHomesAndApartmentsFirst(resSuggestions);
                }
                else if (searchObj.Filter.ToLower().Equals("byBestReviews".ToLower()))
                {
                    filteredSuggestions = OrderByBestGrades(resSuggestions);
                }

                int countSuggestions = filteredSuggestions.Count;

                // PAGINATION
                filteredSuggestions = filteredSuggestions
                    .Skip((pageHelper - 1) * 25)
                    .Take(25)
                    .ToList();

                */

                if (filters is null)
                {
                    return Json(new { code = 400 });
                }

                // FILTERING
                var resSuggestions = _suggestionsFilter.FilterItems(filters.Suggestions, filters.SortOrder);
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

                // PAGINATION
                resSuggestions = _suggestionsPaginator.SelectItems(resSuggestions, filters.Page, filters.PageSize);
                if (resSuggestions is null)
                {
                    return Json(new { code = 400 });
                }

                return Json(new
                {
                    code = 200,
                    suggestions = await resSuggestions.ToListAsync(),
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
