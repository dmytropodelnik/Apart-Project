using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Generators;
using Microsoft.AspNetCore.Mvc;
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

        public CodesController(IGenerator codeGenerator, CodesRepository codesRepository)
        {
            _codeGenerator = codeGenerator;
            _codesRepository = codesRepository;
        }

        [Route("generateregistercode")]
        [HttpPost]
        public IActionResult GenerateRegisterCode([FromBody] string email)
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

        [Route("generateentercode")]
        [HttpGet]
        public IActionResult GenerateEnterCode(string email)
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

            return RedirectToAction("SendVerifyLetter", "Auth", new { emailTrim, code });
        }

        [Route("verifyenteruser")]
        [HttpGet]
        public IActionResult VerifyEnterUser(string email, string code)
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

            _codesRepository.Repository.Remove(email);

            return Json(new { code = 200 });
        }
    }
}
