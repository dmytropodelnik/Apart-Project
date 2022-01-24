﻿using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    // [ApiController]
    public class AuthController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly IEmailSender _emailSender = new AuthEmailSender();
        private string _letterTemplate = "<p>HELLO TEST</p>";

        public AuthController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetLanguages()
        {
            return await _context.Users.ToListAsync();
        }

        [Route("sendregisterletter")]
        [HttpGet]
        public async Task<ActionResult> SendRegisterLetter(string email)
        {
            var res = await _emailSender.SendEmailAsync(email, "Finish subscribing to get deals, inspiration, and more", _letterTemplate);
            if (res == true)
            {
                return Json(new { code = 200 });
            }

            return Json(new { code = 400 });
        }

        // GET: api/<AuthController>
        [Route("sendauthletter")]
        [HttpGet]
        public async Task<ActionResult> SendAuthLetter(string email)
        {
            var res = await _emailSender.SendEmailAsync(email, "Finish subscribing to get deals, inspiration, and more", _letterTemplate);
            if (res == true)
            {
                return Json(new { code = 200 });
            }

            return Json(new { code = 400 });
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
