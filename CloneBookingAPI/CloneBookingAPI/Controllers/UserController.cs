using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    // [ApiController]
    public class UserController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public UserController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("userexists")]
        [HttpGet]
        public async Task<ActionResult<User>> UserExists(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            
            if (user is not null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [Route("getuser")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Email == email);
            if (user is null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
