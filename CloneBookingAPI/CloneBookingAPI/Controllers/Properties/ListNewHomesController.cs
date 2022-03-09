using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.POCOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Properties
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListNewHomesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public ListNewHomesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("addcategory")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null || suggestion.BookingCategory is null)
                {
                    return Json(new { code = 400 });
                }

                var resCategory = await _context.BookingCategories.FirstOrDefaultAsync(c => c.Category.Equals(suggestion.BookingCategory));
                if (resCategory is null)
                {
                    return Json(new { code = 400 });
                }

                Suggestion newSuggestion = new();
                newSuggestion.BookingCategoryId = resCategory.Id;
                newSuggestion.Progress = 10;

                _context.Suggestions.Add(newSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
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

                return Json(new { code = 400 });
            }
        }

        [Route("addcontactdetails")]
        [HttpPost]
        public async Task<IActionResult> AddContactDetails([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null || suggestion.BookingCategory is null)
                {
                    return Json(new { code = 400 });
                }

                var resCategory = await _context.BookingCategories.FirstOrDefaultAsync(c => c.Category.Equals(suggestion.BookingCategory));
                if (resCategory is null)
                {
                    return Json(new { code = 400 });
                }

                Suggestion newSuggestion = new();
                newSuggestion.BookingCategoryId = resCategory.Id;
                newSuggestion.Progress = 10;

                _context.Suggestions.Add(newSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
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

                return Json(new { code = 400 });
            }
        }
    }
}
