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

        [Route("gettypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacilityType>>> GetTypes()
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

        [Route("addtype")]
        [HttpPost]
        public async Task<IActionResult> AddType([FromBody] FacilityType type)
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

        [Route("edittype")]
        [HttpPut]
        public async Task<IActionResult> EditType([FromBody] FacilityType type)
        {
            try
            {
                if (type is null || string.IsNullOrWhiteSpace(type.Type))
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.FacilityTypes.FirstOrDefaultAsync(c => c.Id == type.Id);
                if (resType is null)
                {
                    return Json(new { code = 400 });
                }
                resType.Type = type.Type;

                _context.FacilityTypes.Update(resType);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletetypebyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTypeByName([FromBody] FacilityType type)
        {
            try
            {
                if (type is null || string.IsNullOrWhiteSpace(type.Type))
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.FacilityTypes.FirstOrDefaultAsync(f => f.Type == type.Type);
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
        [HttpDelete]
        public async Task<IActionResult> DeleteType([FromBody] FacilityType type)
        {
            try
            {
                if (type is null || type.Id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.FacilityTypes.FirstOrDefaultAsync(f => f.Id == type.Id);
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
