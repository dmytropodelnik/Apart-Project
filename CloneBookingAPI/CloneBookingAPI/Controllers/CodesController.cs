using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.POCOs;
using CloneBookingAPI.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly CodesRepository _codesRepository;
        private readonly JwtRepository _jwtRepository;

        public CodesController(
            IGenerator codeGenerator, 
            CodesRepository codesRepository,
            JwtRepository jwtRepository)
        {
            _codeGenerator = codeGenerator;
            _codesRepository = codesRepository;
            _jwtRepository = jwtRepository;
        }

        [Route("generateregistercode")]
        [HttpPost]
        public IActionResult GenerateRegisterCode([FromBody] string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                string emailTrim = email.Trim();

                var code = _codeGenerator.GenerateCode(emailTrim);
                if (code is null)
                {
                    return Json(new { code = 400 });
                }

                return RedirectToAction("SendCodeLetter", "Auth", new { emailTrim, code });
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

                var code = _codeGenerator.GenerateCode(emailTrim);
                if (code is null)
                {
                    return Json(new { code = 411 });
                }

                return RedirectToAction("SendVerifyLetter", "Auth", new { emailTrim, code });
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

        [Route("verifyenteruser")]
        [HttpGet]
        public IActionResult VerifyEnterUser(string email, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                bool res = _codesRepository.IsValueCorrect(email, code);
                if (res is false)
                {
                    return Json(new { code = 400 });
                }

                _codesRepository.Repository.Remove(KeyValuePair.Create(email, code));

                return Json(new { code = 200 });
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

        [Route("refreshauth")]
        [HttpPost]
        public IActionResult RefreshAuth([FromBody] TokenModel model)
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

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }
    }
}
