using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Helpers;
using CloneBookingAPI.Services.POCOs;
using CloneBookingAPI.Services.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly JwtRepository _repository;
        private readonly CodesRepository _codesRepository;

        public IdentityController(
            ApartProjectDbContext context,
            SaltGenerator saltGenerator,
            JwtRepository repository,
            CodesRepository codesRepository)
        {
            _context = context;
            _saltGenerator = saltGenerator;
            _repository = repository;
            _codesRepository = codesRepository;
        }

        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> Token([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null || string.IsNullOrWhiteSpace(user.Email))
                {
                    return Json(new { code = 400 });
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

                    bool res = VerifyEnterUser(user.Email, user.Code);
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

                if (_repository.Repository.ContainsKey(user.Email))
                {
                    _repository.Repository[user.Email].Add(encodedJwt);
                }
                else
                {
                    _repository.Repository.Add(user.Email, new List<string> { encodedJwt });
                }

                return Json(encodedJwt);
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

        private bool VerifyEnterUser(string email, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(email))
                {
                    return false;
                }

                bool res = _codesRepository.IsValueCorrect(email, code);
                if (res is false)
                {
                    return false;
                }

                if (_codesRepository.Repository.ContainsKey(email))
                {
                    _codesRepository.Repository[email].Remove(code);

                    if (_codesRepository.Repository[email].Count == 0)
                    {
                        _codesRepository.Repository.Remove(email);
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
    }
}
