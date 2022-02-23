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
        public async Task<ActionResult> GetCategories(string country)
        {
            try
            {
                List<List<Suggestion>> suggestions = new();
                List<List<Suggestion>> citySuggestions = new();

                var categories = await _context.BookingCategories.ToListAsync();
                var suggestionsList = await _context.Suggestions
                    .Include(s => s.Images)
                    .Include(s => s.BookingCategory)
    .               ToListAsync();
                var citiesList = await _context.Cities
                    .Include(c => c.Country)
                    .Include(c => c.Image)
                    .ToListAsync();
                var cities = citiesList
                    .Where(c => c.Country.Title == country)
                    .Take(10)
                    .ToList();

                for (int i = 1; i <= categories.Count; i++)
                {
                    var resSuggestion = suggestionsList
                        .Where(s => s.BookingCategoryId == i)
                        .ToList();

                    suggestions.Add(resSuggestion);
                }

                for (int i = 1; i <= cities.Count; i++)
                {
                    var resCitySuggestion = suggestionsList
                        .Where(s => s.Address.Country.Title == cities[i].Title)
                        .ToList();

                    citySuggestions.Add(resCitySuggestion);
                }

                return Json(new { 
                    code = 200, 
                    categories,
                    cities,
                    suggestions,
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
