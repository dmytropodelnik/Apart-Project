using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<BookingCategory>>> GetCategories(int page = -1, int pageSize = -1)
        {
            try
            {
                List<BookingCategory> categories = new();
                if (page == -1 || pageSize == -1)
                {
                    categories = await _context.BookingCategories.ToListAsync();
                }
                else
                {
                    categories = await _context.BookingCategories
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { code = 200, categories });
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

        [Route("getcategoriesforlist")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingCategory>>> GetCategoriesForList(int categoryTypeId)
        {
            try
            {
                if (categoryTypeId < 1)
                {
                    return Json(new { code = 400 });
                }

                var categories = await _context.BookingCategories
                    .Where(c => c.BookingCategoryTypeId == categoryTypeId)
                    .ToListAsync();
                if (categories is null)
                {
                    return Json(new { code = 400 });
                }

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

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceCategory>>> Search(string category, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category) || page == -1 || pageSize == -1)
                {
                    var res = await _context.BookingCategories.ToListAsync();

                    return Json(new { code = 200, categories = res });
                }

                var categories = await _context.BookingCategories
                    .Where(c => c.Category.Contains(category))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
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
