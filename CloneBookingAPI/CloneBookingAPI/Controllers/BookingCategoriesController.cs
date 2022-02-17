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

                return Json(new { code = 200, categories });
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

        [Route("addcategory")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] BookingCategory category)
        {
            try
            {
                if (category is null || string.IsNullOrWhiteSpace(category.Category))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.BookingCategories.FirstOrDefaultAsync(c => c.Category == category.Category);
                if (res is null)
                {
                    _context.BookingCategories.Add(category);
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

        [Route("editcategory")]
        [HttpPut]
        public async Task<IActionResult> EditCategory([FromBody] BookingCategory category)
        {
            try
            {
                if (category is null || string.IsNullOrWhiteSpace(category.Category))
                {
                    return Json(new { code = 400 });
                }

                var resCategory = await _context.BookingCategories.FirstOrDefaultAsync(c => c.Id == category.Id);
                if (resCategory is null)
                {
                    return Json(new { code = 400 });
                }
                resCategory.Category = category.Category;

                _context.BookingCategories.Update(resCategory);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletecategorybyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryByName([FromBody] BookingCategory category)
        {
            try
            {
                if (category is null || string.IsNullOrWhiteSpace(category.Category))
                {
                    return Json(new { code = 400 });
                }

                var resCategory = await _context.BookingCategories.FirstOrDefaultAsync(c => c.Category == category.Category);
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
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromBody] BookingCategory category)
        {
            try
            {
                if (category is null || category.Id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.BookingCategories.FirstOrDefaultAsync(c => c.Id == category.Id);
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
