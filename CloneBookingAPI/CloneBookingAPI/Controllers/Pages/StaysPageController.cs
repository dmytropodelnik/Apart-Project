using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Suggestions;
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
                    var resplacesOfInterestSuggestion = suggestionsList
                        .Where(s => s.InterestPlaceId == i)
                        .ToList();

                    placesOfInterestSuggestions.Add(resplacesOfInterestSuggestion);
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

                return Json(new { code = ex.Message });
            }
        }

        [Route("getrecommendeddestdata")]
        [HttpGet]
        public async Task<ActionResult> GetRecommendedDestData()
        {
            try
            {
                List<List<Suggestion>> citySuggestions = new();
                
                var citiesList = await _context.Cities
                    .Include(c => c.Country)
                    .Include(c => c.Image)
                    .ToListAsync();

                var cities = citiesList
                    .Take(5)
                    .ToList();

                var resCities = _context.Suggestions
                    .Include(s => s.Address)
                    .Include(s => s.Images)
                    .GroupBy(s => s.Address.City.Title)
                    .OrderBy(s => s.Count())
                    .Take(5);

                //foreach (var item in resCities)
                //{
                //    citySuggestions.Add(item.Key.Count());
                //}
                    
                return Json(new
                {
                    code = 200,
                    citySuggestions,
                });
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

                return Json(new { code = ex.Message });
            }
        }
    }
}
