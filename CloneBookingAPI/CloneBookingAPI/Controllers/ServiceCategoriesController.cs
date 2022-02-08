using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<ActionResult<IEnumerable<ServiceCategory>>> GetCategories()
        {
            try
            {
                return await _context.ServiceCategories.ToListAsync();
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

                var res = await _context.ServiceCategories.FirstOrDefaultAsync(c => c.Category == category);
                if (res is null)
                {
                    ServiceCategory newCategory = new();
                    newCategory.Category = category;
                    _context.ServiceCategories.Add(newCategory);
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

                var resCategory = await _context.ServiceCategories.FirstOrDefaultAsync(c => c.Category == category);
                if (resCategory is null)
                {
                    return Json(new { code = 400 });
                }

                _context.ServiceCategories.Remove(resCategory);
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

                var resCategory = await _context.ServiceCategories.FirstOrDefaultAsync(c => c.Id == id);
                if (resCategory is null)
                {
                    return Json(new { code = 400 });
                }

                _context.ServiceCategories.Remove(resCategory);
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
