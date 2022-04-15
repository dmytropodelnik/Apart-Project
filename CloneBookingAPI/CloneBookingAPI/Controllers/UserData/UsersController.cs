﻿using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Database.Models.UserProfile;
using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.POCOs;
using CloneBookingAPI.Services.Repositories;
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
        private readonly RegistrationCodesRepository _registrationRepository;
        private readonly JwtRepository _jwtRepository;
        private readonly SaltGenerator _saltGenerator;

        public UsersController(
            ApartProjectDbContext context,
            RegistrationCodesRepository registrationRepository,
            SaltGenerator saltGenerator,
            JwtRepository jwtRepository)
        {
            _context = context;
            _registrationRepository = registrationRepository;
            _jwtRepository = jwtRepository;
            _saltGenerator = saltGenerator;
        }

        [Route("userexists")]
        [HttpGet]
        public async Task<IActionResult> UserExists(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user is not null)
                {
                    return RedirectToAction("GenerateEnterCode", "Codes", new { email });
                }

                return Json(new { code = 202, userId = user.Id });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
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

        // [Authorize(Roles = "admin")]
        [Route("getusers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(int page = -1, int pageSize = -1)
        {
            try
            {
                List<User> users = new();
                if (page == -1 || pageSize == -1)
                {
                    users = await _context.Users
                    .Include(u => u.Profile)
                    .Include(u => u.Role)
                    .ToListAsync();
                }
                else
                {
                    users = await _context.Users
                        .Include(u => u.Profile)
                        .Include(u => u.Role)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { code = 200, users });
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Search(string user, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user) || page == -1 || pageSize == -1)
                {
                    var res = await _context.Users
                        .Include(u => u.Profile)
                        .Include(u => u.Role)
                        .ToListAsync();

                    return Json(new { code = 200, users = res });
                }

                var users = await _context.Users
                    .Include(u => u.Profile)
                    .Include(u => u.Role)
                    .Where(u => u.Title.Contains(user) ||
                                u.FirstName.Contains(user) ||
                                u.LastName.Contains(user) ||
                                u.Email.Contains(user) ||
                                u.PhoneNumber.Contains(user) ||
                                u.DisplayName.Contains(user))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Json(new { code = 200, users });
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        // [Authorize(Roles = "admin")]
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
                    .Include(u => u.Profile)
                        .ThenInclude(u => u.Address)
                    .Include(u => u.Profile.Address.Country)
                    .Include(u => u.Profile.Address.City)
                    .Include(u => u.Profile.Currency)
                    .Include(u => u.Profile.Language)
                    .FirstOrDefaultAsync(m => m.Email == email);
                if (user is null)
                {
                    return NotFound();
                }

                return Json(new { 
                    code = 200,
                    user,
                });
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        // [Authorize]
        [Route("getuserproperties")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suggestion>>> GetUserProperties(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return NotFound();
                }

                var suggestions = await _context.Suggestions
                    .Include(r => r.User)
                    .Where(r => r.User.Email.Equals(email))
                    .ToListAsync();

                return Json(new
                {
                    code = 200,
                    suggestions,
                });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
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

        [Route("adduser")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            try
            {
                if (user is null ||
                    string.IsNullOrWhiteSpace(user.Email) ||
                    string.IsNullOrWhiteSpace(user.PasswordHash) ||
                    user.RoleId < 1)
                {
                    return Json(new { code = 400 });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (resUser is not null)
                {
                    return Json(new { code = 400 });
                }

                string hashedPassword = _saltGenerator.GenerateKeyCode(user.PasswordHash.Trim());

                User newUser = new();
                newUser.Email = user.Email.Trim();
                newUser.PasswordHash = hashedPassword;
                newUser.SaltHash = _saltGenerator.Salt;
                newUser.RoleId = user.RoleId;

                Favorite favorite = new();
                var newFavorite = _context.Favorites.Add(favorite);
                newUser.Favorite = favorite;

                _context.Users.Add(newUser);

                UserProfile userProfile = new();
                userProfile.RegisterDate = DateTime.Now.ToUniversalTime();
                userProfile.User = newUser;

                _context.UserProfiles.Add(userProfile);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
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

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CloneBookingAPI.Services.POCOs.PocoData person)
        {
            try
            {
                if (person is null ||
                    string.IsNullOrWhiteSpace(person.Email) ||
                    string.IsNullOrWhiteSpace(person.Password) ||
                    string.IsNullOrWhiteSpace(person.VerificationCode))
                {
                    return Json(new { code = 400 });
                }

                bool res = _registrationRepository.IsValueCorrect(person.Email.Trim(), person.VerificationCode.Trim());
                if (res is false)
                {
                    return Json(new { code = 400 });
                }

                if (_registrationRepository.Repository.ContainsKey(person.Email.Trim()))
                {
                    _registrationRepository.Repository[person.Email.Trim()].Remove(person.Password.Trim());

                    if (_registrationRepository.Repository[person.Email.Trim()].Count == 0)
                    {
                        _registrationRepository.Repository.Remove(person.Email.Trim());
                    }
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == person.Email);
                if (resUser is not null)
                {
                    return Json(new { code = 400 });
                }

                string hashedPassword = _saltGenerator.GenerateKeyCode(person.Password.Trim());

                User newUser = new();
                newUser.Email = person.Email.Trim();
                newUser.PasswordHash = hashedPassword;
                newUser.SaltHash = _saltGenerator.Salt;
                newUser.RoleId = 2;

                Favorite favorite = new();
                var newFavorite = _context.Favorites.Add(favorite);
                newUser.Favorite = favorite;

                _context.Users.Add(newUser);

                UserProfile userProfile = new();
                userProfile.RegisterDate = DateTime.Now.ToUniversalTime();
                userProfile.User = newUser;

                _context.UserProfiles.Add(userProfile);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
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

        [Route("signoutuser")]
        [HttpPost]
        public IActionResult SignOutUser([FromBody] TokenModel model)
        {
            try
            {
                if (model is null ||
                    string.IsNullOrWhiteSpace(model.Username) ||
                    string.IsNullOrWhiteSpace(model.AccessToken))
                {
                    return Json(new { code = 400 });
                }

                bool res = _jwtRepository.IsValueCorrect(model.Username, model.AccessToken);
                if (res is false)
                {
                    return Json(new { code = 400 });
                }

                if (_registrationRepository.Repository.ContainsKey(model.Username))
                {
                    _registrationRepository.Repository[model.Username].Remove(model.AccessToken);

                    if (_registrationRepository.Repository[model.Username].Count == 0)
                    {
                        _registrationRepository.Repository.Remove(model.Username);
                    }
                }

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
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

        // [Authorize]
        [Route("deleteuser")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] CloneBookingAPI.Services.POCOs.PocoData user)
        {
            try
            {
                if (user is null  ||
                    string.IsNullOrWhiteSpace(user.Email))
                {
                    return Json(new { code = 400 });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (resUser is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Users.Remove(resUser);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
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
    }
}
