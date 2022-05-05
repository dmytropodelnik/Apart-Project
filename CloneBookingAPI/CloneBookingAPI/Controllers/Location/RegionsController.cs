using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Location;
using Microsoft.AspNetCore.Http;
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
    public class RegionsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public RegionsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getregions")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegions(int page = -1, int pageSize = -1)
        {
            try
            {
                List<Region> regions = new();
                if (page == -1 || pageSize == -1)
                {
                    regions = await _context.Regions
                    .Include(r => r.City)
                    .Include(d => d.City.Country)
                    .Include(d => d.Image)
                    .ToListAsync();
                }
                else
                {
                    regions = await _context.Regions
                    .Include(r => r.City)
                    .Include(d => d.City.Country)
                    .Include(d => d.Image)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                }

                return Json(new { code = 200, regions });
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
        public async Task<ActionResult<IEnumerable<Region>>> Search(string region, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(region) || page == -1 || pageSize == -1)
                {
                    var res = await _context.Regions.ToListAsync();

                    return Json(new { code = 200, regions = res });
                }

                var regions = await _context.Regions
                    .Include(r => r.City)
                    .Include(d => d.City.Country)
                    .Include(d => d.Image)
                    .Where(r => r.Title.Contains(region))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Json(new { code = 200, regions });
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
        [Route("addregion")]
        [HttpPost]
        public async Task<IActionResult> AddRegion([FromBody] Region region, IFormFile uploadedFile)
        {
            try
            {
                if (region is null || string.IsNullOrWhiteSpace(region.Title))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.Regions.FirstOrDefaultAsync(c => c.Title == region.Title);
                if (res is null)
                {
                    _context.Regions.Add(region);
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
        [Route("changeregionbyname")]
        [HttpPut]
        public async Task<IActionResult> ChangeRegionByName(string region, string newName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(region) || string.IsNullOrWhiteSpace(newName))
                {
                    return Json(new { code = 400 });
                }

                var resRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Title == region);
                if (resRegion is null)
                {
                    return Json(new { code = 400 });
                }
                resRegion.Title = newName;

                _context.Regions.Update(resRegion);
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
        [Route("changeregion")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeRegion(int id, string newName)
        {
            try
            {
                if (id < 1 || string.IsNullOrWhiteSpace(newName))
                {
                    return Json(new { code = 400 });
                }

                var resRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
                if (resRegion is null)
                {
                    return Json(new { code = 400 });
                }
                resRegion.Title = newName;

                _context.Regions.Update(resRegion);
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
        [Route("deleteregionbyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRegionByName(string region)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(region))
                {
                    return Json(new { code = 400 });
                }

                var resRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Title == region);
                if (resRegion is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Regions.Remove(resRegion);
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
        [Route("deleteregion")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
                if (resRegion is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Regions.Remove(resRegion);
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
