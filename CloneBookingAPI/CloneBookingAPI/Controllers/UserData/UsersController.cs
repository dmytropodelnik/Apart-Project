using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
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
        public async Task<IActionResult> UserExists(string email, bool sendLetter = true)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400, message = "Input data is null." });
                }


                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user is not null)
                {
                    if (sendLetter)
                    {
                        return RedirectToAction("GenerateEnterCode", "Codes", new { email });
                    } 
                    else
                    {
                        return Json(new { code = 200, userId = user.Id });
                    }
                }

                return Json(new { code = 202, userId = user.Id });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
        }

        [Route("userexistsbysocial")]
        [HttpGet]
        public async Task<IActionResult> UserExistsBySocial(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
                if (user is null)
                {
                    return Json(new
                    {
                        code = 200,
                        userExisted = false,
                    });
                }

                return Json(new
                {
                    code = 200,
                    userId = user.Id,
                    userExisted = true,
                });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
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

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Search(string user, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user))
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

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("getuser")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400, message = "Input data is null.", });
                }

                var user = await _context.Users
                    .Include(u => u.Profile)
                        .ThenInclude(u => u.Address)
                    .Include(u => u.Profile.Address.Country)
                    .Include(u => u.Profile.Address.City)
                    .Include(u => u.Profile.Currency)
                    .Include(u => u.Profile.Language)
                    .Include(u => u.Profile.Image)
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

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("getuserproperties")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suggestion>>> GetUserProperties(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400, message = "Input data is null.", });
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

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
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
                    return Json(new { code = 400, message = "Input data is null." });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (resUser is not null)
                {
                    return Json(new { code = 400, message = "User is not found." });
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

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
        }

        [Route("registerviasocial")]
        [HttpPost]
        public async Task<IActionResult> RegisterViaSocial([FromBody] PocoData person)
        {
            try
            {
                if (person is null ||
                    string.IsNullOrWhiteSpace(person.Email))
                {
                    return Json(new { code = 400, message = "Input data is null." });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(person.Email));
                if (resUser is not null)
                {
                    return Json(new { code = 400, message = "User is not found." });
                }

                User newUser = new();
                newUser.Email = person.Email;
                newUser.RoleId = 2;

                UserProfile userProfile = new();

                if (!string.IsNullOrWhiteSpace(person.FirstName))
                {
                    newUser.FirstName = person.FirstName;
                }
                if (!string.IsNullOrWhiteSpace(person.LastName))
                {
                    newUser.LastName = person.LastName;
                }
                if (!string.IsNullOrWhiteSpace(person.Image))
                {
                    FileModel image = new();
                    image.Name = person.Image.Substring(person.Image.IndexOf("://") + 3);
                    image.Path = person.Image;

                    _context.Files.Add(image);

                    userProfile.Image = image;
                }

                Favorite favorite = new();
                var newFavorite = _context.Favorites.Add(favorite);
                newUser.Favorite = favorite;

                _context.Users.Add(newUser);

                userProfile.RegisterDate = DateTime.Now.ToUniversalTime();
                userProfile.User = newUser;

                _context.UserProfiles.Add(userProfile);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
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
                    return Json(new { code = 400, message = "Input data is null." });
                }

                bool res = _registrationRepository.IsValueCorrect(person.Email.Trim(), person.VerificationCode.Trim());
                if (res is false)
                {
                    return Json(new { code = 400, message = "Code is not correct." });
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

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
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
                    return Json(new { code = 400, message = "Input data is null." });
                }

                bool res = _jwtRepository.IsValueCorrect(model.Username, model.AccessToken);
                if (res is false)
                {
                    return Json(new { code = 400, message = "Code is not correct." });
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

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("deleteuser")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400, message = "Input data is null." });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (resUser is null)
                {
                    return Json(new { code = 400, message = "User is not found." });
                }

                var resFavorite = await _context.Favorites.FirstOrDefaultAsync(u => u.UserId == resUser.Id);
                if (resFavorite is null)
                {
                    return Json(new { code = 400, message = "Favorite is not found." });
                }

                _context.Favorites.Remove(resFavorite);
                _context.Users.Remove(resUser);

                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("deluser")]
        [HttpDelete]
        public async Task<IActionResult> DelUser(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400, message = "Incorrect id." });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (resUser is null)
                {
                    return Json(new { code = 400, message = "User is not found." });
                }

                var resFavorite = await _context.Favorites.FirstOrDefaultAsync(u => u.UserId == resUser.Id);
                if (resFavorite is null)
                {
                    return Json(new { code = 400, message = "Favorite is not found." });
                }

                _context.Favorites.Remove(resFavorite);
                _context.Users.Remove(resUser);

                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500, message = ex.Message, });
            }
        }
    }
}
