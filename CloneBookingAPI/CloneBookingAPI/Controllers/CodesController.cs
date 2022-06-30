using CloneBookingAPI.Filters;
using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Email;
using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.POCOs;
using CloneBookingAPI.Services.Repositories;
using CloneBookingAPI.Services.Timers;
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
    public class CodesController : Controller
    {
        private readonly IGenerator _codeGenerator;
        private readonly IConfiguration _configuration;
        private readonly RegistrationCodesRepository _registrationRepository;
        private readonly JwtRepository _jwtRepository;
        private readonly DeleteUserCodesRepository _deleteUserRepository;
        private readonly ResetPasswordCodesRepository _resetPasswordRepository;
        private readonly ChangingEmailCodesRepository _changingEmailRepository;
        private readonly EnterCodesRepository _enterRepository;
        private readonly SubscriptionCodesRepository _subscriptionsRepository;
        private readonly ApartProjectDbContext _context;
        private readonly InfoEmailSender _emailSender;

        private readonly string _subjectProfileChangingLetterTemplate = default;

        public CodesController(
            IGenerator codeGenerator, 
            RegistrationCodesRepository registrationRepository,
            JwtRepository jwtRepository,
            DeleteUserCodesRepository deleteUserRepository,
            ResetPasswordCodesRepository resetPasswordRepository,
            ChangingEmailCodesRepository changingEmailRepository,
            EnterCodesRepository enterRepository,
            SubscriptionCodesRepository subscriptionsRepository,
            IConfiguration configuration,
            ApartProjectDbContext context)
        {
            _codeGenerator = codeGenerator;
            _registrationRepository = registrationRepository;
            _jwtRepository = jwtRepository;
            _deleteUserRepository = deleteUserRepository;
            _resetPasswordRepository = resetPasswordRepository;
            _changingEmailRepository = changingEmailRepository;
            _enterRepository = enterRepository;
            _subscriptionsRepository = subscriptionsRepository;
            _configuration = configuration;
            _context = context;
            _emailSender = new InfoEmailSender(configuration);

            _subjectProfileChangingLetterTemplate = configuration["EmailLetterSubjectTemplates:ProfileChaningLetterSubject:Template"];
        }

        [Route("generateregistercode")]
        [HttpGet]
        public IActionResult GenerateRegisterCode(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                string emailTrim = email.Trim();

                var code = _codeGenerator.GenerateKeyCode(emailTrim, _registrationRepository);
                if (code is null)
                {
                    return Json(new { code = 400 });
                }

                new RegistrationCodeCleanTimer(_registrationRepository, _configuration).SetTimer((key: emailTrim, code: code));

                return RedirectToAction("SendCodeLetter", "Auth", new { emailTrim, code });
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("generatechangingemailcode")]
        [HttpGet]
        public IActionResult GenerateChangingEmailCode(string email, string oldEmail)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 410 });
                }

                string emailTrim = email.Trim();
                string oldEmailTrim = oldEmail.Trim();

                var code = _codeGenerator.GenerateKeyCode(emailTrim, _changingEmailRepository);
                if (code is null)
                {
                    return Json(new { code = 411 });
                }

                new ChangingEmailCodeCleanTimer(_changingEmailRepository, _configuration).SetTimer((key: emailTrim, code));

                return RedirectToAction("SendChangingEmailLetter", "Auth", new { emailTrim, oldEmailTrim, code });
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("generatedeleteusercode")]
        [HttpGet]
        public IActionResult GenerateDeleteUserCode(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 410 });
                }

                string emailTrim = email.Trim();

                var code = _codeGenerator.GenerateKeyCode(emailTrim, _deleteUserRepository);
                if (code is null)
                {
                    return Json(new { code = 411 });
                }

                new DeleteUserCodeCleanTimer(_deleteUserRepository, _configuration).SetTimer((key: emailTrim, code));

                return RedirectToAction("SendDeleteUserLetter", "Auth", new { emailTrim, code });
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

        // [TypeFilter(typeof(AuthorizationFilter))]
        [Route("generateresetcode")]
        [HttpGet]
        public IActionResult GenerateResetCode(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 410 });
                }

                string emailTrim = email.Trim();

                var code = _codeGenerator.GenerateKeyCode(emailTrim, _resetPasswordRepository);
                if (code is null)
                {
                    return Json(new { code = 411 });
                }

                new ResetPasswordCodeCleanTimer(_resetPasswordRepository, _configuration).SetTimer((key: emailTrim, code));

                return RedirectToAction("SendResetPasswordLetter", "Auth", new { emailTrim, code });
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

        [Route("generateentercode")]
        [HttpGet]
        public IActionResult GenerateEnterCode(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 410 });
                }

                string emailTrim = email.Trim();

                var code = _codeGenerator.GenerateKeyCode(emailTrim, _enterRepository);
                if (code is null)
                {
                    return Json(new { code = 411 });
                }

                new EnterCodeCleanTimer(_enterRepository, _configuration).SetTimer((key: emailTrim, code));

                return RedirectToAction("SendVerifyEnterLetter", "Auth", new { emailTrim, code });
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

        [Route("generatesubscriptioncode")]
        [HttpGet]
        public async Task<IActionResult> GenerateSubscriptionCode(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 410 });
                }

                var subscriber = await _context.LettersReceivers.FirstOrDefaultAsync(r => r.Receiver.Equals(email));
                if (subscriber is not null)
                {
                    return Json(new { code = 400, message = "You have already subscribed to our deals!" });
                }

                string emailTrim = email.Trim();

                var code = _codeGenerator.GenerateKeyCode(emailTrim, _subscriptionsRepository);
                if (code is null)
                {
                    return Json(new { code = 411 });
                }

                new SubscriptionCodeCleanTimer(_subscriptionsRepository, _configuration).SetTimer((key: emailTrim, code));

                return RedirectToAction("SendSubscriptionLetter", "Auth", new { emailTrim, code });
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

        [Route("verifyuserregistration")]
        [HttpGet]
        public IActionResult VerifyUserRegistration(string email, string code, bool confidant = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                bool res = _registrationRepository.IsValueCorrect(email, code);
                if (res is false)
                {
                    return Json(new { code = 400 });
                }

                if (!confidant)
                {
                    if (_registrationRepository.Repository.ContainsKey(email))
                    {
                        _registrationRepository.Repository[email].Remove(code);

                        if (_registrationRepository.Repository[email].Count == 0)
                        {
                            _registrationRepository.Repository.Remove(email);
                        }
                    }
                }

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

        [Route("verifyenteruser")]
        [HttpGet]
        public IActionResult VerifyEnterUser(string email, string code, bool confidant = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                bool res = _enterRepository.IsValueCorrect(email, code);
                if (res is false)
                {
                    return Json(new { code = 400 });
                }

                if (!confidant)
                {
                    if (_enterRepository.Repository.ContainsKey(email))
                    {
                        _enterRepository.Repository[email].Remove(code);

                        if (_enterRepository.Repository[email].Count == 0)
                        {
                            _enterRepository.Repository.Remove(email);
                        }
                    }
                }

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

        [Route("verifyusersubscription")]
        [HttpGet]
        public IActionResult VerifyUserSubscription(string email, string code, bool confidant = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                bool res = _subscriptionsRepository.IsValueCorrect(email, code);
                if (res is false)
                {
                    return Json(new { code = 400 });
                }

                if (!confidant)
                {
                    if (_subscriptionsRepository.Repository.ContainsKey(email))
                    {
                        _subscriptionsRepository.Repository[email].Remove(code);

                        if (_subscriptionsRepository.Repository[email].Count == 0)
                        {
                            _subscriptionsRepository.Repository.Remove(email);
                        }
                    }
                }

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

        [Route("verifyuserdeletion")]
        [HttpGet]
        public IActionResult VerifyUserDeletion(string email, string code, bool confidant = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                bool res = _deleteUserRepository.IsValueCorrect(email, code);
                if (res is false)
                {
                    return Json(new { code = 400 });
                }

                if (!confidant)
                {
                    if (_deleteUserRepository.Repository.ContainsKey(email))
                    {
                        _deleteUserRepository.Repository[email].Remove(code);

                        if (_deleteUserRepository.Repository[email].Count == 0)
                        {
                            _deleteUserRepository.Repository.Remove(email);
                        }
                    }
                }

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

        [Route("verifyemailchanging")]
        [HttpGet]
        public IActionResult VerifyEmailChanging(string email, string code, bool confidant = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                bool res = _changingEmailRepository.IsValueCorrect(email, code);
                if (res is false)
                {
                    return Json(new { code = 400 });
                }

                if (!confidant)
                {
                    if (_changingEmailRepository.Repository.ContainsKey(email))
                    {
                        _changingEmailRepository.Repository[email].Remove(code);

                        if (_changingEmailRepository.Repository[email].Count == 0)
                        {
                            _changingEmailRepository.Repository.Remove(email);
                        }
                    }
                }

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

        [Route("verifypasswordreset")]
        [HttpGet]
        public IActionResult VerifyPasswordReset(string email, string code, bool confidant = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                bool res = _resetPasswordRepository.IsValueCorrect(email, code);
                if (res is false)
                {
                    return Json(new { code = 400 });
                }

                if (!confidant)
                {
                    if (_resetPasswordRepository.Repository.ContainsKey(email))
                    {
                        _resetPasswordRepository.Repository[email].Remove(code);

                        if (_resetPasswordRepository.Repository[email].Count == 0)
                        {
                            _resetPasswordRepository.Repository.Remove(email);
                        }
                    }
                }

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

        [Route("refreshauth")]
        [HttpPost]
        public async Task<IActionResult> RefreshAuth([FromBody] TokenModel model)
        {
            try
            {
                if (model is null ||
                    string.IsNullOrWhiteSpace(model.AccessToken) ||
                    string.IsNullOrWhiteSpace(model.Username))
                {
                    return Json(new { code = 400 });
                }

                bool res = _jwtRepository.IsValueCorrect(model.Username, model.AccessToken);
                if (res is false)
                {
                    return Json(new { code = 400 });
                }

                var user = await _context.Users
                        .Include(u => u.Profile)
                        .Include(u => u.Profile.Image)
                        .FirstOrDefaultAsync(u => u.Email.Equals(model.Username));
                if (user is null)
                {
                    return Json(new { code = 400 });
                }

                return Json(new
                {
                    code = 200,
                    facebookAuth = model.IsFacebookAuth,
                    googleAuth = model.IsGoogleAuth,
                    user,
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }
    }
}
