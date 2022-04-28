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
                List<int> suggestions = new();

                var suggestionsList = await _context.Suggestions
                    .Include(s => s.Apartments)
                    .ToListAsync();

                var categories = await _context.BookingCategories
                    .Include(c => c.Image)
                    .ToListAsync();

                for (int i = 1; i <= categories.Count; i++)
                {
                    var resSuggestions = suggestionsList
                        .Where(s => s.Progress == 100 && s.Apartments.Count > 0)
                        .Where(s => s.BookingCategoryId == i)
                        .Count();

                    suggestions.Add(resSuggestions);
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
                List<int> regionsSuggestions = new();

                var suggestionsList = await _context.Suggestions
                    .Include(s => s.Address.Region)
                    .ToListAsync();

                var regions = await _context.Regions
                    .Select(r => new { r.Id, r.Title })
                    .Take(20)
                    .ToListAsync();

                for (int i = 1; i <= regions.Count; i++)
                {
                    var resRegionSuggestion = suggestionsList
                        .Where(s => s.Progress == 100 && s.Apartments.Count > 0)
                        .Where(s => s.Address.Region.Id == regions[i - 1].Id)  ///
                        .Count();

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
        public async Task<ActionResult> GetCountriesData()
        {
            try
            {
                List<int> countriesSuggestions = new();

                var suggestionsList = await _context.Suggestions
                    .Include(s => s.Address)
                    .Include(s => s.Address.Country)
                    .ToListAsync();

                var countries = await _context.Countries
                    .Select(c => c.Title)
                    .Take(20)
                    .ToListAsync();

                for (int i = 1; i <= countries.Count; i++)
                {
                    var resCountriesSuggestion = suggestionsList
                        .Where(s => s.Progress == 100 && s.Apartments.Count > 0)
                        .Where(s => s.Address.Country.Id == i)  /// 
                        .Count();

                    countriesSuggestions.Add(resCountriesSuggestion);
                }

                return Json(new
                {
                    code = 200,
                    countries,
                    countriesSuggestions,
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
                List<int> citySuggestionsLength = new();

                var suggestionsList = await _context.Suggestions
                    .Include(s => s.Address.City)
                    .ToListAsync();

                var footerCities = await _context.Cities
                    .Select(c => c.Title)
                    .Take(20)
                    .ToListAsync();

                var cities = await _context.Cities
                    .Include(c => c.Country)
                    .Include(c => c.Image)
                    .Where(c => c.Country.Title == country)
                    .Select(c => new { c.Id, c.Title, c.Image })
                    .Take(10)
                    .ToListAsync();

                for (int i = 1; i <= cities.Count; i++)
                {
                    var resCitySuggestion = suggestionsList
                        .Where(s => s.Progress == 100 && s.Apartments.Count > 0)
                        .Where(s => s.Address.City.Id == cities[i - 1].Id) ////
                        .Count();

                    citySuggestionsLength.Add(resCitySuggestion);
                }
                citySuggestionsLength = citySuggestionsLength
                    .OrderByDescending(c => c)
                    .ToList();

                return Json(new
                {
                    code = 200,
                    cities,
                    footerCities,
                    citySuggestionsLength,
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
                    .Include(c => c.Image)
                    .Include(c => c.Country.Image)
                    .Take(5)
                    .ToListAsync();

                for (int i = 0; i < citiesList.Count; i++)
                {
                    var resCities = await _context.Suggestions
                        .Include(c => c.Address)
                        .Where(s => s.Progress == 100 && s.Apartments.Count > 0)
                        .Where(c => c.Address.CityId == citiesList[i].Id)
                        .CountAsync();

                    suggestionsCount.Add(resCities);
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
                List<decimal> suggestionStartsFrom = new();

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.Images)
                    .Include(s => s.Address.Country)
                    .Include(s => s.Address.City)
                    .Include(s => s.SuggestionReviewGrades)
                    .Include(s => s.Apartments)
                    .Where(s => s.Progress == 100 && s.Apartments.Count > 0)
                    .Where(s => s.SuggestionReviewGrades
                                    .Average(g => g.Value) > 9.0) //  && s.Apartments.Count > 0
                    .Take(5)
                    .ToListAsync();

                for (int i = 0; i < resSuggestion.Count; i++)
                {
                    var resReviews = await _context.Reviews
                        .Where(r => r.SuggestionId == resSuggestion[i].Id)
                        .CountAsync();

                    reviewsCount.Add(resReviews);
                }

                for (int i = 0; i < resSuggestion.Count; i++)
                {
                    if (resSuggestion[i].SuggestionReviewGrades.Count == 0)
                    {
                        suggestionGrades.Add(0);
                    }
                    else
                    {
                        suggestionGrades.Add(resSuggestion[i].SuggestionReviewGrades.Average(g => g.Value));
                    }
                    if (resSuggestion[i].Apartments.Count != 0) // resSuggestion[i].Apartments is not null && 
                    {
                        suggestionStartsFrom.Add(resSuggestion[i].Apartments.Min(a => a.PriceInUSD));
                    }
                }

                return Json(new
                {
                    code = 200,
                    resSuggestion,
                    reviewsCount,
                    suggestionGrades,
                    suggestionStartsFrom,
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
