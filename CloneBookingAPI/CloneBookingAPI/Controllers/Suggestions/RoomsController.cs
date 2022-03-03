using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Suggestions
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public RoomsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getrooms")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            try
            {
                var rooms = await _context.Rooms
                    .Include(r => r.RoomType)
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("editroom")]
        [HttpPut]
        public async Task<IActionResult> EditRoom([FromBody] Room room)
        {
            try
            {
                if (room is null)
                {
                    return Json(new { code = 400 });
                }

                var resRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == room.Id);
                if (resRoom is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Rooms.Update(resRoom);
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
