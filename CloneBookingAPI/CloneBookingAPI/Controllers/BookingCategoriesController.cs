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
                var res = await _context.BookingCategories.ToListAsync();

                return Json(new { code = 200, bookingCategories = res });
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
        public async Task<IActionResult> AddCategory([FromBody] string category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.BookingCategories.FirstOrDefaultAsync(c => c.Category == category);
                if (res is null)
                {
                    BookingCategory newCategory = new();
                    newCategory.Category = category;
                    _context.BookingCategories.Add(newCategory);
                    await _context.SaveChangesAsync();

                    return Json(new { code = 200 });
                }
                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletecategorybyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryByName(string category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    return Json(new { code = 400 });
                }

                var resCategory = await _context.BookingCategories.FirstOrDefaultAsync(c => c.Category == category);
                if (resCategory is null)
                {
                    return Json(new { code = 400 });
                }

                _context.BookingCategories.Remove(resCategory);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletecategory")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.BookingCategories.FirstOrDefaultAsync(c => c.Id == id);
                if (resType is null)
                {
                    return Json(new { code = 400 });
                }

                _context.BookingCategories.Remove(resType);
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
