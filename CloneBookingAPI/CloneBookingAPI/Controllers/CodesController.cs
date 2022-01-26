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

        [Route("generatecode")]
        [HttpPost]
        public IActionResult GenerateCode([FromBody] string email)
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

        // POST api/<ConfirmationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ConfirmationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ConfirmationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
