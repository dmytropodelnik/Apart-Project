using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Review
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewCategoriesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public ReviewCategoriesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getcategories")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewCategory>>> GetCategories()
        {
            try
            {
                var categories = await _context.ReviewCategories
                    .Include(c => c.Suggestion)
                    .ToListAsync();

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
        }

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewCategory>>> Search(string category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    var res = await _context.ReviewCategories.ToListAsync();

                    return Json(new { code = 200, categories = res });
                }

                var categories = await _context.ReviewCategories
                    .Include(c => c.Suggestion)
                    .Where(c => c.Category.Contains(category))
                    .ToListAsync();

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

                return Json(new { code = 400 });
            }
        }
        [Route("addcategory")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] ReviewCategory category)
        {
            try
            {
                if (category is null || string.IsNullOrWhiteSpace(category.Category))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.ReviewCategories.FirstOrDefaultAsync(c => c.Category == category.Category);
                if (res is null)
                {
                    _context.ReviewCategories.Add(category);
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

        [Route("deletecategory")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromBody] ReviewCategory category)
        {
            try
            {
                if (category is null || string.IsNullOrWhiteSpace(category.Category))
                {
                    return Json(new { code = 400 });
                }

                var resCategory = await _context.ReviewCategories.FirstOrDefaultAsync(c => c.Category == category.Category);
                if (resCategory is null)
                {
                    return Json(new { code = 400 });
                }

                _context.ReviewCategories.Remove(resCategory);
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

                var resCategory = await _context.ReviewCategories.FirstOrDefaultAsync(c => c.Id == id);
                if (resCategory is null)
                {
                    return Json(new { code = 400 });
                }

                _context.ReviewCategories.Remove(resCategory);
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
