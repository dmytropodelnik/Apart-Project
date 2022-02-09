using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.UserProfile;
using CloneBookingAPI.Services.Generators;
using Microsoft.AspNetCore.Authorization;
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
    // [ApiController]
    public class UsersController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly CodesRepository _codesRepository;
        private readonly SaltGenerator _saltGenerator;

        public UsersController(
            ApartProjectDbContext context,
            CodesRepository codesRepository,
            SaltGenerator saltGenerator)
        {
            _context = context;
            _codesRepository = codesRepository;
            _saltGenerator = saltGenerator;
        }

        [Route("userexists")]
        [HttpGet]
        public async Task<IActionResult> UserExists(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user is not null)
                {
                    return RedirectToAction("GenerateEnterCode", "Codes", new { email });
                }

                return Json(new { code = 202, enter = false });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        // [Authorize(Roles = "admin")]
        [Route("getusers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var res = await _context.Users.ToListAsync();

                return Json(new { code = 200, users = res });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Authorize(Roles = "admin")]
        [Route("getuser")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("register")]
        [HttpPost]
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

                string hashedPassword = _saltGenerator.GenerateCode(person.Password.Trim());

                User newUser = new();
                newUser.Email = person.Email.Trim();
                newUser.PasswordHash = hashedPassword;
                newUser.SaltHash = _saltGenerator.Salt;
                newUser.RoleId = 2;

                Favorite favorite = new();
                var newFavorite = _context.Favorites.Add(favorite);
                newUser.Favorite = favorite;

                UserProfile userProfile = new();
                userProfile.RegisterDate = DateTime.Now;
                var newUserProfile = _context.UserProfiles.Add(userProfile);
                await _context.SaveChangesAsync();

                newUser.ProfileId = newUserProfile.Entity.Id;

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

        [Authorize]
        [Route("changeusersemail")]
        [HttpPut]
        public async Task<IActionResult> ChangeUsersEmail([FromBody] Services.POCOs.UserData user)
        {
            try
            {
                if (user is null                            ||
                    string.IsNullOrWhiteSpace(user.Email)   ||
                    string.IsNullOrWhiteSpace(user.NewEmail))
                {
                    return Json(new { code = 400 });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (resUser is null)
                {
                    return Json(new { code = 400 });
                }
                resUser.Email = user.NewEmail;

                _context.Users.Update(resUser);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Authorize]
        [Route("changeuserspass")]
        [HttpPut]
        public async Task<IActionResult> ChangeUsersPass([FromBody] Services.POCOs.UserData user)
        {
            try
            {
                if (user is null ||
                    string.IsNullOrWhiteSpace(user.Email) ||
                    string.IsNullOrWhiteSpace(user.NewPassword))
                {
                    return Json(new { code = 400 });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (resUser is null)
                {
                    return Json(new { code = 400 });
                }

                string hashedPassword = _saltGenerator.GenerateCode(user.Password.Trim());

                resUser.PasswordHash = hashedPassword;
                resUser.SaltHash = _saltGenerator.Salt;

                _context.Users.Update(resUser);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Authorize]
        [Route("changeusersphone")]
        [HttpPut]
        public async Task<IActionResult> ChangeUsersPhone([FromBody] Services.POCOs.UserData user)
        {
            try
            {
                if (user is null ||
                    string.IsNullOrWhiteSpace(user.Email) ||
                    string.IsNullOrWhiteSpace(user.PhoneNumber))
                {
                    return Json(new { code = 400 });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (resUser is null)
                {
                    return Json(new { code = 400 });
                }
                resUser.PhoneNumber = user.PhoneNumber;

                _context.Users.Update(resUser);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Authorize]
        [Route("changeusersinfo")]
        [HttpPut]
        public async Task<IActionResult> ChangeUsersInfo([FromBody] User user)
        {
            try
            {
                if (user is null)
                {
                    return Json(new { code = 400 });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (resUser is null)
                {
                    return Json(new { code = 400 });
                }

                if (!string.IsNullOrWhiteSpace(user.Title))
                {
                    resUser.Title = user.Title;
                }
                if (!string.IsNullOrWhiteSpace(user.FirstName))
                {
                    resUser.FirstName = user.FirstName;
                }
                if (!string.IsNullOrWhiteSpace(user.LastName))
                {
                    resUser.LastName = user.LastName;
                }
                if (!string.IsNullOrWhiteSpace(user.DisplayName))
                {
                    resUser.DisplayName = user.DisplayName;
                }

                _context.Users.Update(resUser);
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
