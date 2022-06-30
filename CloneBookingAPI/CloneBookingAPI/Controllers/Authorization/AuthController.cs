using CloneBookingAPI.Filters;
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
        private readonly SaltGenerator _saltGenerator;

        private readonly string _subjectRegistrationLetterTemplate = default;
        private readonly string _subjectVerifyEnterLetterTemplate = default;
        private readonly string _subjectResetPasswordLetterTemplate = default;
        private readonly string _subjectChangeEmailLetterTemplate = default;
        private readonly string _subjectDeleteUserLetterTemplate = default;
        private readonly string _subjectSubscriptionLetterTemplate = default;

        private readonly string _enterLinkTemplate = default;
        private readonly string _resetPasswordLinkTemplate = default;
        private readonly string _changeEmailLinkTemplate = default;
        private readonly string _deleteUserLinkTemplate = default;
        private readonly string _subscriptionLinkTemplate = default;

        public AuthController(ApartProjectDbContext context, IConfiguration configuration, SaltGenerator saltGenerator)
        {
            _context = context;
            _emailSender = new AuthEmailSender(configuration);
            _saltGenerator = saltGenerator;

            _enterLinkTemplate = configuration["EmailLinksTemplates:EnterLetter:Template"];
            _resetPasswordLinkTemplate = configuration["EmailLinksTemplates:ResetPasswordLetter:Template"];
            _changeEmailLinkTemplate = configuration["EmailLinksTemplates:ChangingEmailLetter:Template"];
            _deleteUserLinkTemplate = configuration["EmailLinksTemplates:DeleteUserLetter:Template"];
            _subscriptionLinkTemplate = configuration["EmailLinksTemplates:SubscriptionLetter:Template"];

            _subjectRegistrationLetterTemplate = configuration["EmailLetterSubjectTemplates:RegistrationLetterSubject:Template"];
            _subjectVerifyEnterLetterTemplate = configuration["EmailLetterSubjectTemplates:EnterLetterSubject:Template"];
            _subjectResetPasswordLetterTemplate = configuration["EmailLetterSubjectTemplates:ResetPasswordLetterSubject:Template"];
            _subjectChangeEmailLetterTemplate = configuration["EmailLetterSubjectTemplates:ChangingEmailLetterSubject:Template"];
            _subjectDeleteUserLetterTemplate = configuration["EmailLetterSubjectTemplates:DeleteUserLetterSubject:Template"];
            _subjectSubscriptionLetterTemplate = configuration["EmailLetterSubjectTemplates:SubscriptionLetterSubject:Template"];
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
                    return Json(new { code = 400, message = "Something wrong with email sending." });
                }

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
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
                    return Json(new { code = 400 });
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
                    return Json(new { code = 400, message = "Something wrong with email sending." });
                }

                return Json(new { code = 200 });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("sendresetpasswordletter")]
        [HttpGet]
        public async Task<IActionResult> SendResetPasswordLetter(string emailTrim, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { code = 400, message = "Input data is null." });
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
                    return Json(new { code = 400, message = "Something wrong with email sending." });
                }

                return Json(new { code = 200 });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("sendchangingemailletter")]
        [HttpGet]
        public async Task<IActionResult> SendChangingEmailLetter(string emailTrim, string oldEmailTrim, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { code = 400 });
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
                    return Json(new { code = 400, message = "Something wrong with email sending." });
                }

                return Json(new { code = 200 });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("senddeleteuserletter")]
        [HttpGet]
        public async Task<IActionResult> SendDeleteUserLetter(string emailTrim, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { code = 400, message = "Input data is null." });
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
                    return Json(new { code = 400, message = "Something wrong with email sending." });
                }

                return Json(new { code = 200 });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
        }

        [Route("sendsubscriptionletter")]
        [HttpGet]
        public async Task<IActionResult> SendSubscriptionLetter(string emailTrim, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailTrim) || string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { code = 400 });
                }

                string correctEmail = emailTrim.Trim();

                string linkTemplate = _subscriptionLinkTemplate
                        .Replace("codeTemplate", code);
                linkTemplate = linkTemplate.Replace("emailTemplate", correctEmail);

                bool res = await _emailSender.SendEmailAsync(
                    correctEmail,
                    _subjectSubscriptionLetterTemplate,
                    linkTemplate);
                if (res is false)
                {
                    return Json(new { code = 400, message = "Something wrong with email sending." });
                }

                return Json(new { code = 200 });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
        }

        [Route("loginwithpassword")]
        [HttpGet]
        public async Task<IActionResult> LoginWithPassword(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(password))
                {
                    return Json(new { code = 400, message = "Input data is null." });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.Trim());
                if (resUser is null)
                {
                    return Json(new { code = 400, message = "User is not found." });
                }

                string hashedPassword = _saltGenerator.GenerateKeyCode(password.Trim(), resUser.SaltHash);

                if (hashedPassword == resUser.PasswordHash)
                {
                    return Json(new { code = 200, message = "Login success." });
                }
                return Json(new { code = 400, message = "Incorrect password." });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message });
            }
        }
    }
}
