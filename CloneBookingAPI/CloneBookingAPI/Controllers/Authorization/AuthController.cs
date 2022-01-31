using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Generators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IEmailSender _emailSender;
        private string _letterTemplate = "<p>HELLO TEST</p>";
        private string _subjectLetterTemplate = "Confirmation code for registration!";
        private string _subjectVerifyLetterTemplate = "Verify email for enter!";
        private string _verificationLinkTemplate = "<a href=\"http://localhost:4200/confirmemail?email=emailTemplate&code=codeTemplate\">Verify enter</a>";

        public AuthController(ApartProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _emailSender = new AuthEmailSender(configuration);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetLanguages()
        { 
            return await _context.Users.ToListAsync();
        }

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

        [Route("sendverifyletter")]
        [HttpGet]
        public async Task<IActionResult> SendVerifyLetter(string emailTrim, string code)
        {
            if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
            {
                return Json(new { code = 400 });
            }

            string correctEmail = emailTrim.Trim();
            string linkTemplate = _verificationLinkTemplate
                    .Replace("codeTemplate", code);
            linkTemplate = linkTemplate.Replace("emailTemplate", correctEmail);

            bool res = await _emailSender.SendEmailAsync(
                correctEmail,
                _subjectVerifyLetterTemplate,
                linkTemplate);
            if (res is false)
            {
                return Json(new { code = 400 });
            }

            return Json(new { code = 200 });
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
