using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Suggestions
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public RoomTypesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("gettypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomType>>> GetTypes()
        {
            try
            {
                var rooms = await _context.RoomTypes
                    .Include(t => t.Suggestion)
                    .ToListAsync();

                return Json(new { code = 200, rooms });
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
        public async Task<ActionResult<IEnumerable<RoomType>>> Search(string type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type))
                {
                    var res = await _context.RoomTypes.ToListAsync();

                    return Json(new { code = 200, types = res });
                }

                var types = await _context.RoomTypes
                    .Include(t => t.Suggestion)
                    .Where(t => t.Title.Contains(type))
                    .ToListAsync();

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("addtype")]
        [HttpPost]
        public async Task<IActionResult> AddType([FromBody] RoomType type)
        {
            try
            {
                if (type is null || string.IsNullOrWhiteSpace(type.Title))
                {
                    return Json(new { code = 400 });
                }
                var res = await _context.RoomTypes.FirstOrDefaultAsync(t => t.Title == type.Title);
                if (res is null)
                {

                    _context.RoomTypes.Add(type);
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
        public async Task<IActionResult> EditType([FromBody] RoomType type)
        {
            try
            {
                if (type is null)
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.RoomTypes.FirstOrDefaultAsync(t => t.Id == type.Id);
                if (resType is null)
                {
                    return Json(new { code = 400 });
                }

                _context.RoomTypes.Update(resType);
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
