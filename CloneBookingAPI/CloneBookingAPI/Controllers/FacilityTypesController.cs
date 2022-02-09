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
    public class FacilityTypesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public FacilityTypesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getfacilitytypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacilityType>>> GetFacilityType()
        {
            try
            {
                var types = await _context.FacilityTypes.ToListAsync();

                return Json(new { code = 200, types });
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

        [Route("addfacilitytype")]
        [HttpPost]
        public async Task<IActionResult> AddFacilityType([FromBody] FacilityType type)
        {
            try
            {
                if (type is null || string.IsNullOrWhiteSpace(type.Type))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.FacilityTypes.FirstOrDefaultAsync(f => f.Type == type.Type);
                if (res is null)
                {
                    _context.FacilityTypes.Add(type);
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

        [Route("deletetypebyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTypeByName(string type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type))
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.FacilityTypes.FirstOrDefaultAsync(f => f.Type == type);
                if (resType is null)
                {
                    return Json(new { code = 400 });
                }

                _context.FacilityTypes.Remove(resType);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletetype")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.FacilityTypes.FirstOrDefaultAsync(f => f.Id == id);
                if (resType is null)
                {
                    return Json(new { code = 400 });
                }

                _context.FacilityTypes.Remove(resType);
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
