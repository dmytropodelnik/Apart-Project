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
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    [TypeFilter(typeof(OnlyAdminFilter))]
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

        [Route("sendbestdealsletter")]
        [HttpGet]
        public ActionResult SendBestDealsLetter(MailLetterPoco letter)
        {
            try
            {
                _dealsMailSender.NotifySubscribers(letter);

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

        [Route("addsubscriber")]
        [HttpPost]
        public ActionResult AddSubscriber(string email)
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

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }

        [Route("removesubscriber")]
        [HttpDelete]
        public ActionResult RemoveSubscriber(string email)
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
