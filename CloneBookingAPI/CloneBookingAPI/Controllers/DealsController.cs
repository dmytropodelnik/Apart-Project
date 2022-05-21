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
        private readonly ApartProjectDbContext _context;
        private readonly IEmailSender _dealsMailSender;

        public DealsController(
            IConfiguration configuration,
            ApartProjectDbContext context)
        {
            _context = context;
            _dealsMailSender = new DealsEmailSender(configuration, _context);
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("getmails")]
        [HttpGet]
        public async Task<ActionResult> GetMails(int page = -1, int pageSize = -1)
        {
            try
            {
                List<MailLetter> sentLetters = new();

                if (page == -1 || pageSize == -1)
                {
                    sentLetters = await _context.MailLetters
                        .Include(l => l.Sender)
                        .ToListAsync();
                }
                else
                {
                    sentLetters = await _context.MailLetters
                        .Include(l => l.Sender)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { 
                    code = 200,
                    sentLetters,
                    amount = await _context.MailLetters.CountAsync(),
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
        [Route("sendbestdealsletteragain")]
        [HttpPost]
        public async Task<ActionResult> SendBestDealsLetterAgain(int letterId)
        {
            try
            {
                if (letterId < 1)
                {
                    return Json(new { code = 400 });
                }

                var letter = await _context.MailLetters
                    .Include(l => l.Sender)
                    .FirstOrDefaultAsync(l => l.Id == letterId);
                if (letter is null)
                {
                    return Json(new { code = 400 });
                }

                MailLetterPoco newLetter = new();
                newLetter.Sender = letter.Sender.Email;
                newLetter.Title = letter.Title;
                newLetter.Text = letter.Text;
                newLetter.File = letter.File;
                newLetter.SendingDate = letter.SendingDate;
                newLetter.ReceiversAmount = await _context.LettersReceivers.CountAsync();

                letter.SentCount++;

                _context.MailLetters.Update(letter);
                await _context.SaveChangesAsync();

                _dealsMailSender.NotifySubscribers(newLetter);

                return Json(new { code = 200 });
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("sendbestdealsletter")]
        [HttpPost]
        public async Task<ActionResult> SendBestDealsLetter([FromBody] MailLetterPoco letter)
        {
            try
            {
                if (letter is null)
                {
                    return Json(new { code = 400 });
                }

                var sender = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(letter.Sender));
                if (sender is null)
                {
                    return Json(new { code = 400 });
                }

                MailLetter newMail = new();
                newMail.Title = letter.Title;
                newMail.Text = letter.Text;
                newMail.SenderId = sender.Id;
                newMail.ReceiversAmount = await _context.LettersReceivers.CountAsync();

                _context.MailLetters.Add(newMail);
                await _context.SaveChangesAsync();

                _dealsMailSender.NotifySubscribers(letter);

                return Json(new { 
                    code = 200,
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

                var receiver = await _context.LettersReceivers.FirstOrDefaultAsync(r => r.Receiver.Equals(email));
                if (receiver is not null)
                {
                    return Json(new { code = 400 });
                }

                LettersReceiver newReceiver = new();
                newReceiver.Receiver = email;

                _context.LettersReceivers.Add(newReceiver);

                var user = await _context.Users
                    .Include(u => u.Profile)
                    .FirstOrDefaultAsync(u => u.Email.Equals(email));
                if (user is not null)
                {
                    user.Profile.HasMailing = true;
                    _context.Users.Update(user);
                }

                await _context.SaveChangesAsync();

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

                var receiver = await _context.LettersReceivers.FirstOrDefaultAsync(r => r.Receiver.Equals(email));
                if (receiver is null)
                {
                    return Json(new { code = 400 });
                }

                _context.LettersReceivers.Remove(receiver);

                var user = await _context.Users
                    .Include(u => u.Profile)
                    .FirstOrDefaultAsync(u => u.Email.Equals(email));
                if (user is not null)
                {
                    user.Profile.HasMailing = false;
                    _context.Users.Update(user);
                }

                await _context.SaveChangesAsync();

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
