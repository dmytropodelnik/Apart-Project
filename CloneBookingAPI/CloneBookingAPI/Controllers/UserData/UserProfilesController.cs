﻿using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public UserProfilesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("getprofiles")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfiles(int page = -1, int pageSize = -1)
        {
            try
            {
                List<UserProfile> profiles = new();
                if (page == -1 || pageSize == -1)
                {
                    profiles = await _context.UserProfiles
                    .Include(p => p.User)
                    .Include(p => p.Gender)
                    .Include(p => p.Language)
                    .Include(p => p.Currency)
                    .Include(p => p.Address)
                    .Include(p => p.Image)
                    .ToListAsync();
                }
                else
                {
                    profiles = await _context.UserProfiles
                        .Include(p => p.User)
                        .Include(p => p.Gender)
                        .Include(p => p.Language)
                        .Include(p => p.Currency)
                        .Include(p => p.Address)
                        .Include(p => p.Image)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { code = 200, profiles });
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
        public async Task<ActionResult<IEnumerable<UserProfile>>> Search(string profile, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(profile) || page == -1 || pageSize == -1)
                {
                    var res = await _context.UserProfiles.ToListAsync();

                    return Json(new { code = 200, profiles = res });
                }

                var profiles = await _context.UserProfiles
                    .Where(p => p.BirthDate.ToString().Contains(profile)      ||
                                p.RegisterDate.ToString().Contains(profile)   ||
                                p.Nationality.Contains(profile)               ||
                                p.Gender.Title.Contains(profile)              ||
                                p.Address.Country.Title.Contains(profile)     ||
                                p.Address.City.Title.Contains(profile)        ||
                                p.Address.District.Title.Contains(profile)    ||
                                p.Address.Region.Title.Contains(profile)      ||
                                p.Address.AddressText.Contains(profile)       ||
                                p.Language.Title.Contains(profile)            ||
                                p.User.Email.Contains(profile)                ||
                                p.User.PhoneNumber.Contains(profile))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Json(new { code = 200, profiles });
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

        [Route("getuserprofile")]
        [HttpGet]
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
