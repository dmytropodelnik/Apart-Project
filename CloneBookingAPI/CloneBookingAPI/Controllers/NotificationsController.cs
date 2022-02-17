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
    public class NotificationsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public NotificationsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getnotifications")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            try
            {
                var notifications = await _context.Notifications.ToListAsync();

                return Json(new { code = 200, notifications });
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

        [Route("editnotification")]
        [HttpPut]
        public async Task<IActionResult> EditNotification([FromBody] Notification notification)
        {
            try
            {
                if (notification is null)
                {
                    return Json(new { code = 400 });
                }

                var resNotification = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == notification.Id);
                if (resNotification is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Notifications.Update(resNotification);
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
