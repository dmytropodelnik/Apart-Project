using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public UserProfilesController(
            ApartProjectDbContext context
            )
        {
            _context = context;
        }

        // [Authorize(Roles = "admin")]
        [Route("getuserprofiles")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfiles()
        {
            try
            {
                var res = await _context.UserProfiles.ToListAsync();

                return Json(new { code = 200, profiles = res });
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

        // [Authorize(Roles = "admin")]
        [Route("getuserprofile")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(int id)
        {
            try
            {
                if (id < 1)
                {
                    return NotFound();
                }

                var userProfile = await _context.UserProfiles
                    .FirstOrDefaultAsync(p => p.UserId == id);
                if (userProfile is null)
                {
                    return NotFound();
                }

                return userProfile;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        // [Authorize]
        [Route("changeusersinfo")]
        [HttpPut]
        public async Task<IActionResult> ChangeUsersInfo([FromBody] UserProfile profile)
        {
            try
            {
                if (profile is null)
                {
                    return Json(new { code = 400 });
                }

                var resProfile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == profile.UserId);
                if (resProfile is null)
                {
                    return Json(new { code = 400 });
                }

                //if (!string.IsNullOrWhiteSpace(user.Title))
                //{
                //    resUser.Title = user.Title;
                //}
                //if (!string.IsNullOrWhiteSpace(user.FirstName))
                //{
                //    resUser.FirstName = user.FirstName;
                //}
                //if (!string.IsNullOrWhiteSpace(user.LastName))
                //{
                //    resUser.LastName = user.LastName;
                //}
                //if (!string.IsNullOrWhiteSpace(user.DisplayName))
                //{
                //    resUser.DisplayName = user.DisplayName;
                //}

                _context.UserProfiles.Update(resProfile);
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
