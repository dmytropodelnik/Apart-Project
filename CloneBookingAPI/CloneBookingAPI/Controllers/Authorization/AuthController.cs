using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Generators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly AuthEmailSender _emailSender;
        private string _letterTemplate = "<p>HELLO TEST</p>";
        private string _subjectLetterTemplate = "Confirmation code for registration!";
        private string _subjectVerifyLetterTemplate = "Verify email to enter!";
        private string _subjectResetPasswordLetterTemplate = "Verify email to reset the password!";
        private string _subjectChangeEmailLetterTemplate = "Verify email to change the email!";
        private string _verificationLinkTemplate = "<a href=\"http://localhost:4200/confirmemail?email=emailTemplate&code=codeTemplate\">Verify enter</a>";
        private string _resetPasswordLinkTemplate = "<a href=\"http://localhost:4200/confirmemail?email=emailTemplate&code=codeTemplate&resetPassword=1\">Reset password</a>";
        private string _changeEmailLinkTemplate = "<a href=\"http://localhost:4200/confirmemail?email=emailTemplate&code=codeTemplate&changeEmail=1\">Change email</a>";

        public AuthController(ApartProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _emailSender = new AuthEmailSender(configuration);
        }

        [Route("sendcodeletter")]
        [HttpGet]
        public async Task<IActionResult> SendCodeLetter(string emailTrim, string code)
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("sendverifyletter")]
        [HttpGet]
        public async Task<IActionResult> SendVerifyLetter(string emailTrim, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { code = 415 });
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
                    return Json(new { code = 416 });
                }

                return Json(new { code = 200 });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = ex.Message });
            }
        }

        [Route("sendresetpasswordletter")]
        [HttpGet]
        public async Task<IActionResult> SendResetPasswordLetter(string emailTrim, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { code = 415 });
                }

                string correctEmail = emailTrim.Trim();
                string linkTemplate = _resetPasswordLinkTemplate
                        .Replace("codeTemplate", code);
                linkTemplate = linkTemplate.Replace("emailTemplate", correctEmail);

                bool res = await _emailSender.SendEmailAsync(
                    correctEmail,
                    _subjectResetPasswordLetterTemplate,
                    linkTemplate);
                if (res is false)
                {
                    return Json(new { code = 416 });
                }

                return Json(new { code = 200 });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = ex.Message });
            }
        }

        [Route("sendchangingemailletter")]
        [HttpGet]
        public async Task<IActionResult> SendChangingEmailLetter(string emailTrim, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { code = 415 });
                }

                string correctEmail = emailTrim.Trim();
                string linkTemplate = _changeEmailLinkTemplate
                        .Replace("codeTemplate", code);
                linkTemplate = linkTemplate.Replace("emailTemplate", correctEmail);

                bool res = await _emailSender.SendEmailAsync(
                    correctEmail,
                    _subjectChangeEmailLetterTemplate,
                    linkTemplate);
                if (res is false)
                {
                    return Json(new { code = 416 });
                }

                return Json(new { code = 200 });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = ex.Message });
            }
        }
    }
}
