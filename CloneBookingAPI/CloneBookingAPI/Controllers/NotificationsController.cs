using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public NotificationsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("getnotifications")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications(int page = -1, int pageSize = -1)
        {
            try
            {
                List<Notification> notifications = new();
                if (page == -1 || pageSize == -1)
                {
                    notifications = await _context.Notifications
                    .Include(n => n.EmitterUser)
                    .Include(n => n.Image)
                    .ToListAsync();
                }
                else
                {
                    notifications = await _context.Notifications
                    .Include(n => n.EmitterUser)
                    .Include(n => n.Image)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                }

                return Json(new { 
                    code = 200,
                    notifications,
                });
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

        [Route("getnotifications")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetUserNotifications(string email)
        {
            try
            {
                var notifications = await _context.Notifications
                    .Include(n => n.EmitterUser)
                    .Where(n => n.EmitterUser.Email.Equals(email))
                    .ToListAsync();
                if (notifications is null)
                {
                    return Json(new { code = 400 });
                }

                return Json(new { 
                    code = 200,
                    notifications,
                });
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

        [TypeFilter(typeof(OnlyAdminFilter))]
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
