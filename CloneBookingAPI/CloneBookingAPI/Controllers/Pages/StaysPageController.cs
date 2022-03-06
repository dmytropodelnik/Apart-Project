using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.POCOs.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaysPageController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public StaysPageController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getdata")]
        [HttpGet]
        public async Task<ActionResult> GetData(string country)
        {
            try
            {
                List<List<Suggestion>> suggestions = new();
                List<List<Suggestion>> citySuggestions = new();
                List<List<Suggestion>> regionsSuggestions = new();
                List<List<Suggestion>> placesOfInterestSuggestions = new();

                var placesOfInterests = await _context.InterestPlaces
                    .ToListAsync();
                var categories = await _context.BookingCategories.ToListAsync();
                var footerCities = await _context.Cities
                    .Take(50)
                    .ToListAsync();
                var suggestionsList = await _context.Suggestions
                    .Include(s => s.Images)
                    .Include(s => s.BookingCategory)
                    .Include(s => s.InterestPlaces)
                    .ToListAsync();
                var citiesList = await _context.Cities
                    .Include(c => c.Country)
                    .Include(c => c.Image)
                    .ToListAsync();
                var cities = citiesList
                    .Where(c => c.Country.Title == country)
                    .Take(10)
                    .ToList();
                var regionsList = await _context.Regions
                    .Include(r => r.Address)
                    .Include(r => r.Image)
                    .ToListAsync();
                var regions = regionsList
                    // .Where(r => r.Address.Country.Title == country)
                    .Take(20)
                    .ToList();

                for (int i = 1; i <= categories.Count; i++)
                {
                    var resSuggestion = suggestionsList
                        .Where(s => s.BookingCategoryId == i)
                        .ToList();

                    suggestions.Add(resSuggestion);
                }

                for (int i = 1; i <= citiesList.Count; i++)
                {
                    var resCitySuggestion = suggestionsList
                        .Where(s => s.Address.Country.Title == cities[i].Title)
                        .ToList();

                    citySuggestions.Add(resCitySuggestion);
                }

                for (int i = 1; i <= placesOfInterests.Count; i++)
                {
                    var resPlacesOfInterestSuggestion = suggestionsList
                        .Where(s => s.InterestPlaces
                                        .All(p => p.Id == i))
                        .ToList();

                    placesOfInterestSuggestions.Add(resPlacesOfInterestSuggestion);
                }

                for (int i = 1; i <= cities.Count; i++)
                {
                    var resCitySuggestion = suggestionsList
                        .Where(s => s.Address.Country.Title == cities[i].Title)
                        .ToList();

                    citySuggestions.Add(resCitySuggestion);
                }

                for (int i = 1; i <= regions.Count; i++)
                {
                    var resRegionSuggestion = suggestionsList
                        .Where(s => s.Address.Country.Title == cities[i].Title)
                        .ToList();

                    regionsSuggestions.Add(resRegionSuggestion);
                }

                return Json(new
                {
                    code = 200,
                    categories,
                    cities,
                    regions,
                    suggestions,
                    citySuggestions,
                    footerCities,
                    placesOfInterestSuggestions,
                    placesOfInterests,
                    regionsSuggestions,
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

        [Route("getrecommendeddestdata")]
        [HttpGet]
        public async Task<ActionResult> GetRecommendedDestData()
        {
            try
            {
                List<int> suggestionsCount = new();

                var citiesList = await _context.Cities
                    .Include(c => c.Country)
                    .Include(c => c.Image)
                    // .DistinctBy(c => c.Country.Title)
                    .Take(5)
                    .ToListAsync();

                for (int i = 0; i < citiesList.Count; i++)
                {
                    var resCities = await _context.Suggestions
                        .Include(c => c.Address)
                        .Where(c => c.Address.CityId == citiesList[i].Id)
                        .ToListAsync();

                    suggestionsCount.Add(resCities.Count);
                }

                return Json(new
                {
                    code = 200,
                    citiesList,
                    suggestionsCount,
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

        [Route("getguestslovedata")]
        [HttpGet]
        public async Task<ActionResult> GetGuestsLoveData()
        {
            try
            {
                List<int> reviewsCount = new();

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.Address)
                    .Include(s => s.Reviews)
                    .Include(s => s.SuggestionReviewGrades)
                    .Where(s => s.SuggestionReviewGrades
                                    .All(g => g.Value >= 9.0))
                    .Take(5)
                    .ToListAsync();

                for (int i = 0; i < resSuggestion.Count; i++)
                {
                    var resReviews = await _context.Reviews
                        .Include(r => r.Suggestion)
                        .Where(r => r.SuggestionId == resSuggestion[i].Id)
                        .ToListAsync();

                    reviewsCount.Add(resReviews.Count);
                }

                return Json(new
                {
                    code = 200,
                    resSuggestion,
                    reviewsCount,
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
