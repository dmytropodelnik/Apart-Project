using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CloneBookingAPI.Services.Database.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class AdminController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly SHA256 sha256 = SHA256.Create();

        public AdminController(ApartProjectDbContext context)
        {
            _context = context;
        }

        // GET: api/<AdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AdminController>
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User person)
        {
            
            if (string.IsNullOrWhiteSpace(person.Email) || string.IsNullOrWhiteSpace(person.Password))
            {
                return Json(new { code = 400 });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => 
                                    u.Email == person.Email &&
                                    u.Password == Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(person.Password)))
            );

            if (user is null)
            {
                return Json(new { code = 400 });
            }

            return Json(new { code = 200 });
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
