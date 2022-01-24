using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    // [ApiController]
    public class DealsController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly IEmailSender _emailSender = new DealsEmailSender();
        private string _letterTemplate = "<p>HELLO TEST</p>";

        public DealsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        // GET: api/<AuthController>
        [Route("sendbestdealsemail")]
        [HttpGet]
        public async Task<ActionResult> Get(string email)
        {
            var res = await _emailSender.SendEmailAsync(email, "Finish subscribing to get deals, inspiration, and more", _letterTemplate);
            if (res == true)
            {
                return Json(new { code = 200 });
            }

            return Json(new { code = 400 });
        }
    }
}
