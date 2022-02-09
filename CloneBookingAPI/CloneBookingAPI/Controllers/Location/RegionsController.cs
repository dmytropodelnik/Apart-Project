using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Location;
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
    public class RegionsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public RegionsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getregions")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
        {
            try
            {
                var regions = await _context.Regions.ToListAsync();

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
        }

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deleteregion")]
        [HttpDelete("{id}")]
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }
    }
}
