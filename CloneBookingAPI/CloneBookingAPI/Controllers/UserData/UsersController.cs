using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.UserProfile;
using CloneBookingAPI.Services.Generators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly CodesRepository _codesRepository;
        private readonly SHA256 sha256 = SHA256.Create();

        public UsersController(ApartProjectDbContext context, CodesRepository codesRepository)
        {
            _context = context;
            _codesRepository = codesRepository;
        }

        [Route("userexists")]
        [HttpGet]
        public async Task<IActionResult> UserExists(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            
            if (user is not null)
            {
                return RedirectToAction("GenerateEnterCode", "Codes", new { email });
            }

            return Json(new { code = 202, enter = false });
        }

        [Route("getusers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(string email)
        {
            return await _context.Users.ToListAsync();
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

        [Route("register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] Services.POCOs.UserData person)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(person.Email) ||
                    string.IsNullOrWhiteSpace(person.Password) ||
                    string.IsNullOrWhiteSpace(person.VerificationCode))
                {
                    return Json(new { code = 400 });
                }

                bool res = _codesRepository.IsValueCorrect(person.Email.Trim(), person.VerificationCode.Trim());
                if (res is false)
                {
                    return Json(new { code = 400 });
                }

                _codesRepository.Repository.Remove(person.Email.Trim());

                User newUser = new();
                newUser.Email = person.Email.Trim();
                newUser.Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(person.Password.Trim())));
                newUser.RoleId = 2;

                Favorite favorite = new();
                var newFavorite = _context.Favorites.Add(favorite);
                newUser.FavoriteId = newFavorite.Entity.Id;

                UserProfile userProfile = new();
                userProfile.RegisterDate = DateTime.Now;
                var newUserProfile = _context.UserProfiles.Add(userProfile);
                await _context.SaveChangesAsync();

                newUser.ProfileId = newUserProfile.Entity.Id;
                newUser.FavoriteId = newFavorite.Entity.Id;

                var addedUser = _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                var updateProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.Id == newUser.ProfileId);
                updateProfile.UserId = addedUser.Entity.Id;
                await _context.SaveChangesAsync();

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
