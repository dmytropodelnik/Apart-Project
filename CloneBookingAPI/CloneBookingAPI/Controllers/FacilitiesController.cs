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
    public class FacilitiesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public FacilitiesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getfacilities")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facility>>> GetFacilities()
        {
            try
            {
                var facilities = await _context.Facilities.ToListAsync();

                return Json(new { code = 200, facilities });
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

        [Route("addfacility")]
        [HttpPost]
        public async Task<IActionResult> AddFacility([FromBody] Facility facility)
        {
            try
            {
                if (facility is null || string.IsNullOrWhiteSpace(facility.Text))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.Facilities.FirstOrDefaultAsync(f => f.Text == facility.Text);
                if (res is null)
                {
                    _context.Facilities.Add(facility);
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

        [Route("deletefacilitybyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFacilityByName(string facility)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(facility))
                {
                    return Json(new { code = 400 });
                }

                var resFacility = await _context.Facilities.FirstOrDefaultAsync(f => f.Text == facility);
                if (resFacility is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Facilities.Remove(resFacility);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletefacility")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resFacility = await _context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
                if (resFacility is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Facilities.Remove(resFacility);
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
