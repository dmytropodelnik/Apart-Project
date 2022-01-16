using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Helpers;
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

		public IdentityController(ApartProjectDbContext context)
		{
			_context = context;
		}

		[Route("token")]
		[HttpPost]
		public async Task<IActionResult> Token([FromBody] User user)
		{
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

            return Json(encodedJwt);
		}
		private async Task<IReadOnlyCollection<Claim>> GetIdentity(string email, string password)
		{
			List<Claim> claims = null;

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user is not null)
            {
                SHA256 sha256 = SHA256.Create();
                var passwordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));

                if (passwordHash == user.Password)
                {
                    claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                    };
                }
            }

            return claims;
		}
	}
}
