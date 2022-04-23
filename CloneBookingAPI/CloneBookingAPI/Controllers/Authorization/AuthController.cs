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

        private readonly string _subjectRegistrationLetterTemplate  = default;
        private readonly string _subjectVerifyEnterLetterTemplate   = default;
        private readonly string _subjectResetPasswordLetterTemplate = default;
        private readonly string _subjectChangeEmailLetterTemplate   = default;
        private readonly string _subjectDeleteUserLetterTemplate    = default;

        private readonly string _enterLinkTemplate          = default;
        private readonly string _resetPasswordLinkTemplate  = default;
        private readonly string _changeEmailLinkTemplate    = default;
        private readonly string _deleteUserLinkTemplate     = default;

        public AuthController(ApartProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _emailSender = new AuthEmailSender(configuration);

            _enterLinkTemplate         = configuration["EmailLinksTemplates:EnterLetter:Template"];
            _resetPasswordLinkTemplate = configuration["EmailLinksTemplates:ResetPasswordLetter:Template"];
            _changeEmailLinkTemplate   = configuration["EmailLinksTemplates:ChangingEmailLetter:Template"];
            _deleteUserLinkTemplate    = configuration["EmailLinksTemplates:DeleteUserLetter:Template"];

            _subjectRegistrationLetterTemplate  = configuration["EmailLetterSubjectTemplates:RegistrationLetterSubject:Template"];
            _subjectVerifyEnterLetterTemplate   = configuration["EmailLetterSubjectTemplates:EnterLetterSubject:Template"];
            _subjectResetPasswordLetterTemplate = configuration["EmailLetterSubjectTemplates:ResetPasswordLetterSubject:Template"];
            _subjectChangeEmailLetterTemplate   = configuration["EmailLetterSubjectTemplates:ChangingEmailLetterSubject:Template"];
            _subjectDeleteUserLetterTemplate    = configuration["EmailLetterSubjectTemplates:DeleteUserLetterSubject:Template"];
        }


        [Route("isemailregistered")]
        [HttpGet]
        public async Task<IActionResult> IsEmailRegistered(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Json(new
                {
                    code = 400,
                    message = "Email parameter is null or whitespace",
                });
            }

            var isEmailExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (isEmailExists is not null)
            {
                return Json(new
                {
                    code = 200,
                    isExisted = true,
                    message = "This email is already registered!",
                });
            }

            return Json(new
            {
                code = 200,
                isExisted = false,
                message = "",
            });
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

                bool res = await _emailSender.SendEmailAsync(correctEmail, _subjectRegistrationLetterTemplate, code);
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
        public async Task<IActionResult> SendVerifyEnterLetter(string emailTrim, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { code = 415 });
                }

                string correctEmail = emailTrim.Trim();
                string linkTemplate = _enterLinkTemplate
                        .Replace("codeTemplate", code);
                linkTemplate = linkTemplate.Replace("emailTemplate", correctEmail);

                bool res = await _emailSender.SendEmailAsync(
                    correctEmail,
                    _subjectVerifyEnterLetterTemplate,
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
        public async Task<IActionResult> SendChangingEmailLetter(string emailTrim, string oldEmailTrim, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { code = 415 });
                }

                string correctEmail = emailTrim.Trim();
                string correctOldEmail = oldEmailTrim.Trim();

                string linkTemplate = _changeEmailLinkTemplate
                        .Replace("codeTemplate", code);
                linkTemplate = linkTemplate.Replace("emailTemplate", correctEmail);
                linkTemplate = linkTemplate.Replace("oldEmailTemplate", correctOldEmail);

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

        [Route("senddeleteuserletter")]
        [HttpGet]
        public async Task<IActionResult> SendDeleteUserLetter(string emailTrim, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { code = 415 });
                }

                string correctEmail = emailTrim.Trim();

                string linkTemplate = _deleteUserLinkTemplate
                        .Replace("codeTemplate", code);
                linkTemplate = linkTemplate.Replace("emailTemplate", correctEmail);

                bool res = await _emailSender.SendEmailAsync(
                    correctEmail,
                    _subjectDeleteUserLetterTemplate,
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
