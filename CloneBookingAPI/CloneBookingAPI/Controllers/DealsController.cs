﻿using CloneBookingAPI.Filters;
using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    [TypeFilter(typeof(AuthFilter))]
    [Route("api/[controller]")]
    // [ApiController]
    public class DealsController : Controller
    {
        private readonly ApartProjectDbContext  _context;
        private readonly IEmailSender           _emailSender;
        private string _letterTemplate = "<p>HELLO TEST</p>";

        public DealsController(ApartProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _emailSender = new DealsEmailSender(configuration);
        }

        // GET: api/<AuthController>
        [Route("sendbestdealsletter")]
        [HttpGet]
        public async Task<ActionResult> SendBestDealsLetter(string email)
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