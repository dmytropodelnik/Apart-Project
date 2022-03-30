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
        public async Task<ActionResult<IEnumerable<RoomType>>> GetTypes(int page = -1, int pageSize = -1)
        {
            try
            {
                List<RoomType> types = new();
                if (page == -1 || pageSize == -1)
                {
                    types = await _context.RoomTypes
                    //.Include(t => t.Suggestions)
                    .ToListAsync();
                }
                else
                {
                    types = await _context.RoomTypes
                    //.Include(t => t.Suggestions)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                }

                return Json(new { code = 200, types });
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

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomType>>> Search(string type, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type) || page == -1 || pageSize == -1)
                {
                    var res = await _context.RoomTypes.ToListAsync();

                    return Json(new { code = 200, types = res });
                }

                var types = await _context.RoomTypes
                    //.Include(t => t.Suggestions)
                    .Where(t => t.Title.Contains(type))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Json(new { code = 200, types });
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
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
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
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
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
    }
}
