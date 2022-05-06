using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Helpers;
using CloneBookingAPI.Services.POCOs;
using CloneBookingAPI.Services.Repositories;
using CloneBookingAPI.Services.Timers;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    public class IdentityController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly SaltGenerator _saltGenerator;
        private readonly JwtRepository _jwtRepository;
        private readonly RegistrationCodesRepository _registrationRepository;
        private readonly EnterCodesRepository _enterRepository;
        private readonly ResetPasswordCodesRepository _resetPasswordRepository;
        private readonly ChangingEmailCodesRepository _changingEmailRepository;
        private readonly DeleteUserCodesRepository _deleteUserRepository;
        private readonly JwtCodeCleaner _jwtCodeCleaner;
        private readonly IConfiguration _configuration;

        private BaseRepository _repository = null;

        public IdentityController(
            ApartProjectDbContext context,
            SaltGenerator saltGenerator,
            JwtRepository jwtRepository,
            RegistrationCodesRepository registrationRepository,
            EnterCodesRepository enterRepository,
            ResetPasswordCodesRepository resetPasswordRepository,
            ChangingEmailCodesRepository changingEmailRepository,
            DeleteUserCodesRepository deleteUserRepository,
            JwtCodeCleaner jwtCodeCleaner,
            IConfiguration configuration)
        {
            _context = context;
            _saltGenerator = saltGenerator;
            _jwtRepository = jwtRepository;
            _registrationRepository = registrationRepository;
            _enterRepository = enterRepository;
            _resetPasswordRepository = resetPasswordRepository;
            _changingEmailRepository = changingEmailRepository;
            _deleteUserRepository = deleteUserRepository;
            _jwtCodeCleaner = jwtCodeCleaner;
            _configuration = configuration;
        }

        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> Token([FromBody] CloneBookingAPI.Services.POCOs.PocoData user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.FacebookId))
                {
                    if (user is null || string.IsNullOrWhiteSpace(user.Email))
                    {
                        return Json(new { code = 400 });
                    }
                }

                if (!string.IsNullOrWhiteSpace(user.Code) &&
                    string.IsNullOrWhiteSpace(user.Password))
                {
                   
                    var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(user.Email));
                    if (resUser is null)
                    {
                        return Json(new { code = 400 });
                    }

                    user.Password = resUser.PasswordHash;

                    bool res = VerifyUser(user);
                    if (!res)
                    {
                        return Json(new { code = 400 });
                    }
                }

                var claims = await GetIdentity(user.Email, user.Password);
                if (claims is null)
                {
                    return Unauthorized();
                }

                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                if (encodedJwt is null)
                {
                    return Json(new { code = 400 });
                }

                if (_jwtRepository.Repository.ContainsKey(user.Email))
                {
                    _jwtRepository.Repository[user.Email].Add(encodedJwt);
                }
                else
                {
                    _jwtRepository.Repository.Add(user.Email, new List<string> { encodedJwt });
                }

                // new JwtCodeCleanTimer(_jwtRepository, _configuration).SetTimer((key: user.Email, code: encodedJwt));

                return Json(new { 
                    code = 200,
                    encodedJwt,
                    user.Email,
                });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (SecurityTokenEncryptionFailedException ex)
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

        [Route("tokenforfacebook")]
        [HttpPost]
        public async Task<IActionResult> TokenForFacebook([FromBody] CloneBookingAPI.Services.POCOs.PocoData user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.FacebookId))
                {
                    return Json(new { code = 400 });
                }

                var resUser = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.FacebookId.Equals(user.FacebookId));
                if (resUser is null)
                {
                    return Json(new { code = 400 });
                }

                var claims = GetIdentityForFacebook(resUser.FacebookId, resUser.Role.Name);
                if (claims is null)
                {
                    return Unauthorized();
                }

                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                if (encodedJwt is null)
                {
                    return Json(new { code = 400 });
                }

                if (_jwtRepository.Repository.ContainsKey(resUser.FacebookId))
                {
                    _jwtRepository.Repository[user.FacebookId].Add(encodedJwt);
                }
                else
                {
                    _jwtRepository.Repository.Add(user.FacebookId, new List<string> { encodedJwt });
                }

                // new JwtCodeCleanTimer(_jwtRepository, _configuration).SetTimer((key: user.Email, code: encodedJwt));

                return Json(new
                {
                    code = 200,
                    encodedJwt,
                    resUser.FacebookId,
                });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (SecurityTokenEncryptionFailedException ex)
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

        private async Task<IReadOnlyCollection<Claim>> GetIdentity(string email, string password)
        {
            try
            {
                List<Claim> claims = default;
                string hashedPassword = default;

                var user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (user is not null)
                {
                    hashedPassword = user.PasswordHash;
                }
                else
                {
                    hashedPassword = _saltGenerator.GenerateKeyCode(password);
                }

                if (hashedPassword == user.PasswordHash)
                {
                    claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                    };
                }
                return claims;
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }

        private IReadOnlyCollection<Claim> GetIdentityForFacebook(string id, string role)
        {
            try
            {
                List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, id),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                    };
                return claims;
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }

        private bool VerifyUser(CloneBookingAPI.Services.POCOs.PocoData user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.Code) || string.IsNullOrWhiteSpace(user.Email))
                {
                    return false;
                }

                SetRepository(user);

                if (_repository is null)
                {
                    return false;
                }

                bool res = _repository.IsValueCorrect(user.Email, user.Code);
                if (res is false)
                {
                    return false;
                }

                if (user.IsToDeleteCode)
                {
                    if (_repository.Repository.ContainsKey(user.Email))
                    {
                        _repository.Repository[user.Email].Remove(user.Code);

                        if (_repository.Repository[user.Email].Count == 0)
                        {
                            _repository.Repository.Remove(user.Email);
                        }
                    }
                }

                return true;
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
        }
        void SetRepository(CloneBookingAPI.Services.POCOs.PocoData user)
        {
            switch (user.Repository)
            {
                case Enums.RepositoryEnum.Registration:

                    _repository = _registrationRepository;

                    break;
                case Enums.RepositoryEnum.Enter:

                    _repository = _enterRepository;

                    break;
                case Enums.RepositoryEnum.ResetPassword:

                    _repository = _resetPasswordRepository;

                    break;
                case Enums.RepositoryEnum.ChangingEmail:

                    _repository = _changingEmailRepository;

                    break;
                case Enums.RepositoryEnum.UserDeletion:

                    _repository = _deleteUserRepository;

                    break;
            }
        }
    }
}
