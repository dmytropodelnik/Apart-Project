using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingCategoriesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public BookingCategoriesController(ApartProjectDbContext context)
        {
            _context = context;
        }


        [Route("getcategories")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingCategory>>> GetCategories()
        {
            try
            {
                var categories = await _context.BookingCategories.ToListAsync();
                if (categories is null)
                {
                    return Json(new { code = 400 });
                }

                return Json(new { code = 200, bookingCategories = categories });
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
        }

        [Route("addcategory")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] BookingCategory newCategory, IFormFile uploadedFile)
        {
            try
            {
                if (newCategory is null)
                {
                    return Json(new { code = 400 });
                }

                var resCategory = await _context.BookingCategories.FirstOrDefaultAsync(c => c.Category == newCategory.Category);
                if (resCategory is not null)
                {
                    return Json(new { code = 400, message = "Category alredy exists" });
                }

                BookingCategory category = new();
                category.Category = newCategory.Category;

                _context.BookingCategories.Add(category);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }
    }
}
