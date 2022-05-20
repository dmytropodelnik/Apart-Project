using CloneBookingAPI.Filters;
using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Email;
using CloneBookingAPI.Services.POCOs;
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
        private readonly ApartProjectDbContext  _context;
        private readonly IEmailSender           _dealsMailSender;
        private string _letterTemplate = "<p>HELLO TEST</p>";

        public DealsController(ApartProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _dealsMailSender = new DealsEmailSender(configuration);
        }

        [Route("sendbestdealsletter")]
        [HttpGet]
        public async Task<ActionResult> SendBestDealsLetter(MailLetterPoco letter)
        {
            try
            {
                var res = await _emailSender.SendEmailAsync(let, "Finish subscribing to get deals, inspiration, and more", _letterTemplate);
                if (res == true)
                {
                    return Json(new { code = 200 });
                }

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
