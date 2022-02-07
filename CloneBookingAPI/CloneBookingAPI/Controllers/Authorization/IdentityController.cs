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
using System.IdentityModel.Tokens.Jwt;
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

        public IdentityController(
            ApartProjectDbContext context,
            SaltGenerator saltGenerator)
        {
            _context = context;
            _saltGenerator = saltGenerator;

        }

        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> Token([FromBody] User user)
        {
            var claims = await GetIdentity(user.Email, user.PasswordHash);
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

            // _repository.Repository.Add(user.Email, encodedJwt);
            // TokenModel tokenModel = new TokenModel(user.Email, encodedJwt);


            return Json(encodedJwt);
        }
        private async Task<IReadOnlyCollection<Claim>> GetIdentity(string email, string password)
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
                hashedPassword = _saltGenerator.GenerateCode(password);
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
    }
}
