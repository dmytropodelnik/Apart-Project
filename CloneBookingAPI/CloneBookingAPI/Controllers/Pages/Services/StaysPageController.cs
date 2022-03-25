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

        [Route("getcategoriesdata")]
        [HttpGet]
        public async Task<ActionResult> GetCategoriesData(string country)
        {
            try
            {
                List<List<Suggestion>> suggestions = new();

                var suggestionsList = await _context.Suggestions
                    .Include(s => s.Images)
                    .Include(s => s.Address)
                        .ThenInclude(c => c.Country)
                    .Include(s => s.Address.Region)
                    .Include(s => s.BookingCategory)
                    .Include(s => s.InterestPlaces)
                    .ToListAsync();

                var categories = await _context.BookingCategories
                    .ToListAsync();

                for (int i = 1; i <= categories.Count; i++)
                {
                    var resSuggestion = suggestionsList
                        .Where(s => s.BookingCategoryId == i)
                        .ToList();

                    suggestions.Add(resSuggestion);
                }

                return Json(new
                {
                    code = 200,
                    categories,
                    suggestions,
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

        [Route("getregionsdata")]
        [HttpGet]
        public async Task<ActionResult> GetRegionsData()
        {
            try
            {
                List<List<Suggestion>> regionsSuggestions = new();

                var suggestionsList = await _context.Suggestions
                    .Include(s => s.Images)
                    .Include(s => s.Address)
                        .ThenInclude(c => c.Country)
                    .Include(s => s.Address.Region)
                    .Include(s => s.BookingCategory)
                    .Include(s => s.InterestPlaces)
                    .ToListAsync();

                var regionsList = await _context.Regions
                    .Include(r => r.City)
                    .Include(r => r.Image)
                    .ToListAsync();
                var regions = regionsList
                    .Take(20)
                    .ToList();

                for (int i = 1; i <= regions.Count; i++)
                {
                    var resRegionSuggestion = suggestionsList
                        .Where(s => s.Address.Region.Id == regions[i - 1].Id)
                        .ToList();

                    regionsSuggestions.Add(resRegionSuggestion);
                }

                return Json(new
                {
                    code = 200,
                    regions,
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

        [Route("getinterestplacesdata")]
        [HttpGet]
        public async Task<ActionResult> GetInterestPlacesData()
        {
            try
            {
                List<List<Suggestion>> placesOfInterestSuggestions = new();

                var suggestionsList = await _context.Suggestions
                    .Include(s => s.Images)
                    .Include(s => s.Address)
                        .ThenInclude(c => c.Country)
                    .Include(s => s.Address.Region)
                    .Include(s => s.BookingCategory)
                    .Include(s => s.InterestPlaces)
                    .ToListAsync();

                var placesOfInterests = await _context.InterestPlaces
                    .ToListAsync();

                for (int i = 1; i <= placesOfInterests.Count; i++)
                {
                    var resPlacesOfInterestSuggestion = suggestionsList
                        .Where(s => s.InterestPlaces
                                        .Any(p => p.Id == i))
                        .ToList();

                    placesOfInterestSuggestions.Add(resPlacesOfInterestSuggestion);
                }

                return Json(new
                {
                    code = 200,
                    placesOfInterests,
                    placesOfInterestSuggestions,
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

        [Route("getcitiesdata")]
        [HttpGet]
        public async Task<ActionResult> GetCitiesData(string country)
        {
            try
            {
                List<List<Suggestion>> citySuggestions = new();

                var suggestionsList = await _context.Suggestions
                    .Include(s => s.Images)
                    .Include(s => s.Address)
                        .ThenInclude(c => c.Country)
                    .Include(s => s.Address.Region)
                    .Include(s => s.BookingCategory)
                    .Include(s => s.InterestPlaces)
                    .ToListAsync();

                var citiesList = await _context.Cities
                    .Include(c => c.Country)
                    .Include(c => c.Image)
                    .ToListAsync();

                var footerCities = citiesList
                    .Take(50)
                    .ToList();

                var cities = citiesList
                    .Where(c => c.Country.Title == country)
                    .Take(10)
                    .ToList();

                for (int i = 1; i <= cities.Count; i++)
                {
                    var resCitySuggestion = suggestionsList
                        .Where(s => s.Address.City.Id == cities[i - 1].Id)
                        .ToList();

                    citySuggestions.Add(resCitySuggestion);
                }
                citySuggestions = citySuggestions
                    .OrderByDescending(c => c.Count)
                    .ToList();

                return Json(new
                {
                    code = 200,
                    cities,
                    footerCities,
                    citySuggestions,
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
                List<double> suggestionGrades = new();

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.Address)
                    .Include(s => s.Reviews)
                    .Include(s => s.SuggestionReviewGrades)
                    .Where(s => s.SuggestionReviewGrades
                                    .Average(g => g.Value) > 9.0)
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

                for (int i = 0; i < resSuggestion.Count; i++)
                {
                    suggestionGrades.Add(resSuggestion[i].SuggestionReviewGrades.Average(g => g.Value));                    
                }

                return Json(new
                {
                    code = 200,
                    resSuggestion,
                    reviewsCount,
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
    }
}
