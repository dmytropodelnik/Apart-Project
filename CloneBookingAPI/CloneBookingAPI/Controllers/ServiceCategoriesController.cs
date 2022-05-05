using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoriesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public ServiceCategoriesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getcategories")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceCategory>>> GetCategories(int page = -1, int pageSize = -1)
        {
            try
            {
                List<ServiceCategory> categories = new();
                if (page == -1 || pageSize == -1)
                {
                    categories = await _context.ServiceCategories.ToListAsync();
                }
                else
                {
                    categories = await _context.ServiceCategories
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
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

                return Json(new { code = 400 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceCategory>>> Search(string category, int page = 1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    var res = await _context.ServiceCategories.ToListAsync();

                    return Json(new { code = 200, categories = res });
                }

                var categories = await _context.ServiceCategories
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("addcategory")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] ServiceCategory category)
        {
            try
            {
                if (category is null || string.IsNullOrWhiteSpace(category.Category))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.ServiceCategories.FirstOrDefaultAsync(c => c.Category == category.Category);
                if (res is null)
                {
                    _context.ServiceCategories.Add(category);
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
        public async Task<IActionResult> EditCategory([FromBody] ServiceCategory category)
        {
            try
            {
                if (category is null || string.IsNullOrWhiteSpace(category.Category))
                {
                    return Json(new { code = 400 });
                }

                var resCategory = await _context.ServiceCategories.FirstOrDefaultAsync(c => c.Id == category.Id);
                if (resCategory is null)
                {
                    return Json(new { code = 400 });
                }
                resCategory.Category = category.Category;

                _context.ServiceCategories.Update(resCategory);
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
        public async Task<IActionResult> DeleteCategoryByName(string category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    return Json(new { code = 400 });
                }

                var resCategory = await _context.ServiceCategories.FirstOrDefaultAsync(c => c.Category == category);
                if (resCategory is null)
                {
                    return Json(new { code = 400 });
                }

                _context.ServiceCategories.Remove(resCategory);
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
        public async Task<IActionResult> DeleteCategory([FromBody] ServiceCategory category)
        {
            try
            {
                if (category is null || category.Id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resCategory = await _context.ServiceCategories.FirstOrDefaultAsync(c => c.Id == category.Id);
                if (resCategory is null)
                {
                    return Json(new { code = 400 });
                }

                _context.ServiceCategories.Remove(resCategory);
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
