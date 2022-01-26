using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Generators;
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
<<<<<<< HEAD
        private string _subjectLetterTemplate = "Confirmation code for registration!";
=======
>>>>>>> fad238bf51c380eaffc6456711e6e23a65b78def

        public AuthController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetLanguages()
        { 
            return await _context.Users.ToListAsync();
        }

<<<<<<< HEAD
        [Route("sendcodeletter")]
        [HttpGet]
        public async Task<IActionResult> SendCodeLetter(string emailTrim, string code)
        {
            if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
            {
                return Json(new { code = 400 });
            }

            string correctEmail = emailTrim.Trim();

            bool res = await _emailSender.SendEmailAsync(correctEmail, _subjectLetterTemplate, code);
            if (res is false)
            {
                return Json(new { code = 400 });
            }

            return Json(new { code = 200 });
        }

=======
>>>>>>> fad238bf51c380eaffc6456711e6e23a65b78def
        [Route("sendregisterletter")]
        [HttpGet]
        public async Task<ActionResult> SendRegisterLetter(string email)
        {
<<<<<<< HEAD
            string emailTrim = email.Trim();

            var res = await _emailSender.SendEmailAsync(emailTrim, "Finish subscribing to get deals, inspiration, and more", _letterTemplate);
            if (res is true)
=======
            var res = await _emailSender.SendEmailAsync(email, "Finish subscribing to get deals, inspiration, and more", _letterTemplate);
            if (res == true)
>>>>>>> fad238bf51c380eaffc6456711e6e23a65b78def
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
<<<<<<< HEAD
            if (res is true)
=======
            if (res == true)
>>>>>>> fad238bf51c380eaffc6456711e6e23a65b78def
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
