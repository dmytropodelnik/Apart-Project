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
                    .Include(s => s.Highlights)
                    .Include(s => s.Languages)
                    .Include(s => s.Images)
                    .Include(s => s.Reviews)
                        .ThenInclude(r => r.Grades)
                    .Where(s => s.Progress == 100)
                    .ToListAsync();

                string place = filters.Place ?? "";

                List<int> amountFilteringSuggestions = new();

                // FILTERING
                var resSuggestions = _suggestionsFilter.FilterItems(suggestions.AsQueryable(), filters.Filters, amountFilteringSuggestions);
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

                var finalSuggestions = resSuggestions.ToList();

                List<double> suggestionGrades = new();
                List<decimal> suggestionStartsFrom = new();
                for (int i = 0; i < finalSuggestions.Count; i++)
                {
                    if (finalSuggestions[i].Reviews.Count == 0)
                    {
                        suggestionGrades.Add(0);
                    }
                    else 
                    {
                        suggestionGrades.Add(finalSuggestions[i].Reviews.Average(r => r.Grades.Average(g => g.Value)));
                    } 
                    if (finalSuggestions[i].Apartments.Count != 0) // finalSuggestions[i].Apartments is not null && 
                    {
                        suggestionStartsFrom.Add(finalSuggestions[i].Apartments.Min(a => a.PriceInUSD));
                    }
                }

                return Json(new
                {
                    code = 200,
                    resSuggestions = resSuggestions
                        .Select(s => new { s.Id, s.UniqueCode, s.Name, s.Description, country = s.Address.Country.Title, city = s.Address.City.Title, region = s.Address.Region.Title, address = s.Address.AddressText, starsRating = new short[s.StarsRating], 
                          reviews = s.Reviews.Count, images = s.Images.Select(i => new { i.Path, i.Name }) }),
                    suggestionsAmount,
                    suggestionStartsFrom,
                    suggestionGrades,
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
