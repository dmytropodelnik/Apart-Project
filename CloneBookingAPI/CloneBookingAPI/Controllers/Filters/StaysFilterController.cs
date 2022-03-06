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

namespace CloneBookingAPI.Controllers.Filters
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaysFilterController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public StaysFilterController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getbookingcategories")]
        [HttpGet]
        public async Task<ActionResult> GetBookingCategories()
        {
            try
            {
                List<List<Suggestion>> suggestions = new();
                List<BookingCategory> bookingCategories = new();

                var categories = await _context.BookingCategories.ToListAsync();

                var suggestionsList = await _context.Suggestions
                        .Include(s => s.Images)
                        .Include(s => s.BookingCategory)
                        .Include(s => s.InterestPlaces)
                        .ToListAsync();

                for (int i = 1; i <= categories.Count; i++)
                {
                    var resSuggestion = suggestionsList
                        .Where(s => s.BookingCategoryId == i)
                        .ToList();

                    if (resSuggestion.Count != 0)
                    {
                        bookingCategories.Add(categories[i]);
                    }
                    suggestions.Add(resSuggestion);
                }

                return Json(new
                {
                    code = 200,
                    categories,
                    suggestions,
                    bookingCategories,
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

        [Route("getbookingcategoriesbyprice")]
        [HttpGet]
        public async Task<ActionResult> GetBookingCategoriesByPrice(string country)
        {
            try
            {
                List<List<Suggestion>> suggestions = new();

                var categories = await _context.BookingCategories.ToListAsync();

                var suggestionsList = await _context.Suggestions
                        .Include(s => s.Images)
                        .Include(s => s.BookingCategory)
                        .Include(s => s.InterestPlaces)
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
    }
}
