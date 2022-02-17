using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Authorization;
using CloneBookingAPI.Services.Generators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    //[ApiController]
    public class AdminController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly SaltGenerator _saltGenerator;

        public AdminController(
            ApartProjectDbContext context,
            SaltGenerator saltGenerator)
        {
            _context = context;
            _saltGenerator = saltGenerator;
        }

        // POST api/<AdminController>
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User person)
        {

            if (string.IsNullOrWhiteSpace(person.Email) || string.IsNullOrWhiteSpace(person.PasswordHash))
            {
                return Json(new { code = 400 });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u =>
                                    u.Email == person.Email &&
                                    u.RoleId == 1);
            string hashedPassword = _saltGenerator.GeneratePassHash(
                            person.PasswordHash,
                            user.SaltHash);

            if (user is null || hashedPassword != user.PasswordHash)
            {
                return Json(new { code = 400 });
            }

            return Json(new { code = 200 });
        }
    }
}
