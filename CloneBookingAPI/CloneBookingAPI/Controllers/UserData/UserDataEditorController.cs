﻿using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Generators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.UserData
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataEditorController : Controller
    {
        private readonly ApartProjectDbContext _context;

        private readonly SaltGenerator _saltGenerator;

        public UserDataEditorController(
            ApartProjectDbContext context,
            SaltGenerator saltGenerator)
        {
            _context = context;
            _saltGenerator = saltGenerator;
        }

        [Route("edittitle")]
        [HttpPut]
        public async Task<IActionResult> EditTitle([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null                          || 
                    string.IsNullOrWhiteSpace(user.Title) ||
                    string.IsNullOrWhiteSpace(user.Email))
                {
                    return Json(new { code = 400 });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(user.Email));
                if (resUser is null)
                {
                    return Json(new { code = 400 });
                }

                resUser.Title = user.Title;

                _context.Users.Update(resUser);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
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

        [Route("editname")]
        [HttpPut]
        public async Task<IActionResult> EditName([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null                              ||
                    string.IsNullOrWhiteSpace(user.FirstName) ||
                    string.IsNullOrWhiteSpace(user.LastName)  ||
                    string.IsNullOrWhiteSpace(user.Email))
                {
                    return Json(new { code = 400 });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(user.Email));
                if (resUser is null)
                {
                    return Json(new { code = 400 });
                }

                resUser.FirstName = user.FirstName;
                resUser.LastName = user.LastName;

                _context.Users.Update(resUser);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
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

        [Route("editdisplayname")]
        [HttpPut]
        public async Task<IActionResult> EditDisplayName([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null                                ||
                    string.IsNullOrWhiteSpace(user.DisplayName) ||
                    string.IsNullOrWhiteSpace(user.Email))
                {
                    return Json(new { code = 400 });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(user.Email));
                if (resUser is null)
                {
                    return Json(new { code = 400 });
                }

                resUser.DisplayName = user.DisplayName;

                _context.Users.Update(resUser);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
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

        // [Authorize]
        [Route("editemail")]
        [HttpPut]
        public async Task<IActionResult> EditEmail([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null                          ||
                    string.IsNullOrWhiteSpace(user.Email) ||
                    string.IsNullOrWhiteSpace(user.NewEmail))
                {
                    return Json(new { code = 400 });
                }

                var isEmailExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.NewEmail);
                if (isEmailExists is not null)
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
        [Route("editpassword")]
        [HttpPut]
        public async Task<IActionResult> EditPassword([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null                            ||
                    string.IsNullOrWhiteSpace(user.Email)   ||
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
        [Route("editphonenumber")]
        [HttpPut]
        public async Task<IActionResult> EditPhoneNumber([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null                          ||
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

                var isPhoneExists = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber);
                if (resUser is not null)
                {
                    return Json(new { code = 400 });
                }

                resUser.PhoneNumber = user.PhoneNumber;

                _context.Users.Update(resUser);
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

        // [Authorize]
        [Route("editbirthdate")]
        [HttpPut]
        public async Task<IActionResult> EditBirthDate([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null           ||
                    user.BirthDate is null ||
                    string.IsNullOrWhiteSpace(user.Email))
                {
                    return Json(new { code = 400 });
                }

                var resProfile = await _context.UserProfiles
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.User.Email.Equals(user.Email));
                if (resProfile is null)
                {
                    return Json(new { code = 400 });
                }

                resProfile.BirthDate = Convert.ToDateTime(user.BirthDate).ToUniversalTime();

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

        [Route("editnationality")]
        [HttpPut]
        public async Task<IActionResult> EditNationality([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null ||
                    string.IsNullOrWhiteSpace(user.Nationality) ||
                    string.IsNullOrWhiteSpace(user.Email))
                {
                    return Json(new { code = 400 });
                }

                var resProfile = await _context.UserProfiles
                                    .Include(p => p.User)
                                    .FirstOrDefaultAsync(p => p.User.Email.Equals(user.Email));
                if (resProfile is null)
                {
                    return Json(new { code = 400 });
                }

                resProfile.Nationality = user.Nationality;

                _context.UserProfiles.Update(resProfile);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
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

        [Route("editgender")]
        [HttpPut]
        public async Task<IActionResult> EditGender([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null ||
                    user.GenderId < 1 ||
                    string.IsNullOrWhiteSpace(user.Email))
                {
                    return Json(new { code = 400 });
                }

                var resProfile = await _context.UserProfiles
                                    .Include(p => p.User)
                                    .FirstOrDefaultAsync(p => p.User.Email.Equals(user.Email));
                if (resProfile is null)
                {
                    return Json(new { code = 400 });
                }

                resProfile.GenderId = user.GenderId;

                _context.UserProfiles.Update(resProfile);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
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

        [Route("editaddress")]
        [HttpPut]
        public async Task<IActionResult> EditAddress([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null ||
                    string.IsNullOrWhiteSpace(user.Address.AddressText) &&
                    string.IsNullOrWhiteSpace(user.Address.Country.Title) &&
                    string.IsNullOrWhiteSpace(user.Address.City.Title) &&
                    string.IsNullOrWhiteSpace(user.Address.ZipCode))
                {
                    return Json(new { code = 400 });
                }

                var resProfile = await _context.UserProfiles
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.User.Email.Equals(user.Email));
                if (resProfile is null)
                {
                    return Json(new { code = 400 });
                }

                var resAddress = await _context.Addresses
                    .Include(a => a.UserProfile)
                    .FirstOrDefaultAsync(a => a.UserProfile.Id == resProfile.Id);
                if (resAddress is null)
                {
                    resAddress.UserProfile = resProfile;

                    _context.Addresses.Add(resAddress);
                }
                else
                {
                    resAddress = user.Address;

                    _context.Addresses.Update(resAddress);
                }
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

        [Route("editlanguage")]
        [HttpPut]
        public async Task<IActionResult> EditLanguage([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null                             ||
                    string.IsNullOrWhiteSpace(user.Language) ||
                    string.IsNullOrWhiteSpace(user.Email))
                {
                    return Json(new { code = 400 });
                }

                var resProfile = await _context.UserProfiles
                    .Include(p => p.User)
                    .Include(p => p.Language)
                    .FirstOrDefaultAsync(p => p.User.Email.Equals(user.Email));
                if (resProfile is null)
                {
                    return Json(new { code = 400 });
                }

                var resLanguage = await _context.Languages.FirstOrDefaultAsync(l => l.Title.Equals(user.Language));
                if (resLanguage is null)
                {
                    return Json(new { code = 400 });
                }

                resProfile.LanguageId = resLanguage.Id;

                _context.UserProfiles.Update(resProfile);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
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

        [Route("editcurrency")]
        [HttpPut]
        public async Task<IActionResult> EditCurrency([FromBody] CloneBookingAPI.Services.POCOs.UserData user)
        {
            try
            {
                if (user is null || user.Currency is null)
                {
                    return Json(new { code = 400 });
                }

                var resProfile = await _context.UserProfiles
                    .Include(p => p.User)
                    .Include(p => p.Currency)
                    .FirstOrDefaultAsync(p => p.User.Email.Equals(user.Email));
                if (resProfile is null)
                {
                    return Json(new { code = 400 });
                }

                var resCurrency = await _context.Currencies.FirstOrDefaultAsync(c => c.Abbreviation.Equals(user.Currency.Abbreviation) &&
                                                                                     c.Value.Equals(user.Currency.Value));
                if (resCurrency is null)
                {
                    return Json(new { code = 400 });
                }

                resProfile.CurrencyId = resCurrency.Id;

                _context.UserProfiles.Update(resProfile);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
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
    }
}