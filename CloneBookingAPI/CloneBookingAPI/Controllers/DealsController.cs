using CloneBookingAPI.Database.Models;
using CloneBookingAPI.Filters;
using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Email;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.POCOs;
using CloneBookingAPI.Services.Repositories;
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
    // [ApiController]
    public class DealsController : Controller
    {
        private readonly MailUserListRepository _repository;
        private readonly ApartProjectDbContext _context;
        private readonly IEmailSender _dealsMailSender;

        public DealsController(
            IConfiguration configuration,
            ApartProjectDbContext context,
            MailUserListRepository repository)
        {
            _context = context;
            _dealsMailSender = new DealsEmailSender(configuration, repository);
            _repository = repository;
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("getmails")]
        [HttpGet]
        public async Task<ActionResult> GetMails()
        {
            try
            {
                var sentLetters = await _context.MailLetters
                    .Include(l => l.Sender)
                    .ToListAsync();

                return Json(new { 
                    code = 200,
                    sentLetters,
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("sendbestdealsletter")]
        [HttpPost]
        public async Task<ActionResult> SendBestDealsLetter([FromBody] MailLetterPoco letter)
        {
            try
            {
                var sender = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(letter.Sender));
                if (sender is null)
                {
                    return Json(new { code = 400 });
                }

                MailLetter newMail = new();
                newMail.Title = letter.Title;
                newMail.Text = letter.Text;
                newMail.SenderId = sender.Id;
                newMail.ReceiversAmount = _repository.Subscribers.Count;

                _context.MailLetters.Add(newMail);
                await _context.SaveChangesAsync();

                _dealsMailSender.NotifySubscribers(letter);

                return Json(new { 
                    code = 200, 
                    sentLetters = await _context.MailLetters.ToListAsync(),
                });
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

        [Route("addsubscriber")]
        [HttpPost]
        public async Task<ActionResult> AddSubscriber(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                if (_repository.Subscribers.Contains(email))
                {
                    return Json(new { code = 400 });
                }

                _repository.Subscribers.Add(email);

                var user = await _context.Users
                    .Include(u => u.Profile)
                    .FirstOrDefaultAsync(u => u.Email.Equals(email));
                if (user is not null)
                {
                    user.Profile.HasMailing = true;
                }

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("removesubscriber")]
        [HttpDelete]
        public async Task<ActionResult> RemoveSubscriber(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                if (!_repository.Subscribers.Contains(email))
                {
                    return Json(new { code = 400 });
                }

                _repository.Subscribers.Remove(email);

                var user = await _context.Users
                    .Include(u => u.Profile)
                    .FirstOrDefaultAsync(u => u.Email.Equals(email));
                if (user is not null)
                {
                    user.Profile.HasMailing = false;
                }

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }
    }
}
