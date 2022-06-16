using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly InfoEmailSender _emailSender;

        private readonly string _subjectProfileActionLetterTemplate = default;
        private readonly string _subjectProfileChangingLetterTemplate = default;
        private readonly string _subjectNewBookingLetterTemplate = default;

        public NotificationsController(
            ApartProjectDbContext context,
            IConfiguration configuration)
        {
            _context = context;

            _emailSender = new InfoEmailSender(configuration);


            _subjectProfileChangingLetterTemplate = configuration["EmailLetterSubjectTemplates:ProfileChaningLetterSubject:Template"];
            _subjectProfileActionLetterTemplate = configuration["EmailLetterSubjectTemplates:ProfileActionLetterSubject:Template"];
            _subjectNewBookingLetterTemplate = configuration["EmailLetterSubjectTemplates:NewBookingLetterSubject:Template"];
        }

        [TypeFilter(typeof(AuthorizationFilter))]
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

                return Json(new
                {
                    code = 200,
                    notifications,
                });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
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

                return Json(new
                {
                    code = 200,
                    notifications,
                });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
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

                return Json(new { code = 500, message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
        }

        [Route("sendnotification")]
        [HttpGet]
        public async Task<IActionResult> SendNotification(string email, string message, bool action = true)
        {
            try
            {
                bool res = false;

                if (action)
                {
                    res = await _emailSender.SendEmailAsync(email, _subjectProfileActionLetterTemplate, message);
                }
                else
                {
                    res = await _emailSender.SendEmailAsync(email, _subjectProfileChangingLetterTemplate, message);
                }
                if (res is false)
                {
                    return Json(new { code = 400, message = "Something wrong with email sending." });
                }

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
        }

        [Route("sendsuccessbookingmail")]
        [HttpGet]
        public async Task<IActionResult> SendSuccessBookingMail(string email, string message)
        {
            try
            {
                bool res = await _emailSender.SendEmailAsync(email, _subjectNewBookingLetterTemplate, message);
                if (res is false)
                {
                    return Json(new { code = 400, message = "Something wrong with email sending." });
                }

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
        }
    }
}
