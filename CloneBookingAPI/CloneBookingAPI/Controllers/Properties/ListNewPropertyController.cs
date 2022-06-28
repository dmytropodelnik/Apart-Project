using CloneBookingAPI.Database.Models.Suggestions;
using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Files;
using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.POCOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Suggestions
{
    [TypeFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class ListNewPropertyController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly SuggestionIdGenerator _suggestionIdGenerator;

        private readonly IWebHostEnvironment _appEnvironment;
        private readonly SHA256 sha256 = SHA256.Create();
        private readonly FileUploader _fileUploader;

        public ListNewPropertyController(
            ApartProjectDbContext context,
            SuggestionIdGenerator suggestionIdGenerator,
            IWebHostEnvironment appEnvironment,
            FileUploader fileUploader)
        {
            _context = context;
            _suggestionIdGenerator = suggestionIdGenerator;
            _appEnvironment = appEnvironment;
            _fileUploader = fileUploader;
        }

        [Route("addbookingcategory")]
        [HttpPost]
        public async Task<IActionResult> AddBookingCategory([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.BookingCategoryId < 1)
                {
                    return Json(new { code = 400 });
                }

                var owner = await _context.Users.FirstOrDefaultAsync(u => u.Email == suggestion.Login);
                if (owner is null)
                {
                    return Json(new { code = 400 });
                }

                Suggestion newSuggestion = new();
                newSuggestion.UserId = owner.Id;
                newSuggestion.BookingCategoryId = suggestion.BookingCategoryId;
                newSuggestion.UniqueCode = await _suggestionIdGenerator.GenerateCodeAsync();
                newSuggestion.ServiceCategoryId = 1;
                newSuggestion.Progress = 5;
                newSuggestion.SuggestionStatusId = 3;

                var resSuggestion = _context.Suggestions.Add(newSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Entity.Id,
                });
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

        [Route("addname")]
        [HttpPost]
        public async Task<IActionResult> AddName([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.Id < 1  ||
                    string.IsNullOrWhiteSpace(suggestion.Name))
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.Name = suggestion.Name;
                resSuggestion.Progress = 10;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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

        [Route("addaddress")]
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null || 
                    suggestion.Address.CountryId < 1                         ||
                    string.IsNullOrWhiteSpace(suggestion.Login)              ||
                    string.IsNullOrWhiteSpace(suggestion.Address.City.Title) ||
                    string.IsNullOrWhiteSpace(suggestion.Address.AddressText))
                {
                    return Json(new { code = 400 });
                }

                var owner = await _context.Users.FirstOrDefaultAsync(u => u.Email == suggestion.Login);
                if (owner is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                // ???
                var resCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Id == suggestion.Address.CountryId);
                if (resCountry is null)
                {
                    return Json(new { code = 400 });
                }

                City newCity = new();
                Region newRegion = new();
                Address newAddress = new();

                var region = await _context.Regions.FirstOrDefaultAsync(r => r.Title.Equals(suggestion.Address.Region.Title));
                if (region is null)
                {
                    newRegion.Title = suggestion.Address.Region.Title;
                    newRegion.City = newCity;
                    newRegion.ImageId = resCountry.ImageId;
                }

                if (region is null)
                {
                    newAddress.Region = newRegion;
                }
                else
                {
                    newAddress.RegionId = region.Id;
                }

                newAddress.CountryId = suggestion.Address.CountryId;
                newAddress.AddressText = suggestion.Address.AddressText;

                var city = await _context.Cities.FirstOrDefaultAsync(c => c.Title.Equals(suggestion.Address.City.Title) &&
                                                                          c.CountryId == suggestion.Address.CountryId);
                if (city is null)
                {
                    newCity.Title = suggestion.Address.City.Title;
                    newCity.CountryId = suggestion.Address.CountryId;
                    newCity.ImageId = resCountry.ImageId;  // ???

                    newAddress.City = newCity;
                }
                else
                {
                    newAddress.CityId = city.Id;
                }
                resSuggestion.Address = newAddress;
                resSuggestion.Progress = 15;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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

        [Route("addapartments")]
        [HttpPost]
        public async Task<IActionResult> AddApartments([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.Apartments is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                foreach (var item in suggestion.Apartments)
                {
                    Apartment newApartment = new();
                    newApartment.Name = item.Name;
                    newApartment.Description = item.Description;
                    newApartment.PriceInUSD = item.PriceInUSD;
                    newApartment.RoomsAmount = item.RoomsAmount;
                    newApartment.GuestsLimit = item.GuestsLimit;
                    newApartment.BathroomsAmount = item.BathroomsAmount;

                    resSuggestion.Apartments.Add(newApartment);
                }

                resSuggestion.Progress = 25;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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

        [Route("addfacilities")]
        [HttpPost]
        public async Task<IActionResult> AddFacilities([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.Facilities is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                List<int> facilitiesIds = new();
                for (int i = 0; i < suggestion.Facilities.Count; i++)
                {
                    if (suggestion.Facilities[i])
                    {
                        facilitiesIds.Add(i + 1);
                    }
                }

                var resFacilities = await _context.Facilities
                    .Where(f => facilitiesIds.Contains(f.Id))
                    .ToListAsync();

                resSuggestion.Facilities = resFacilities;
                resSuggestion.Progress = 30;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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

        [Route("addparking")]
        [HttpPost]
        public async Task<IActionResult> AddParking([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.IsParkingAvailable = suggestion.IsParkingAvailable;
                resSuggestion.Progress = 40;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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

        [Route("addlanguages")]
        [HttpPost]
        public async Task<IActionResult> AddLanguages([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.SuggestionRules is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resLanguages = await _context.Languages
                    .Where(f => suggestion.Languages.Contains(f.Title))
                    .ToListAsync();

                resSuggestion.Languages = resLanguages;
                resSuggestion.Progress = 45;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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

        [Route("addrules")]
        [HttpPost]
        public async Task<IActionResult> AddRules([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.SuggestionRules is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                List<int> rulesIds = new();
                for (int i = 0; i < suggestion.SuggestionRules.Count; i++)
                {
                    if (suggestion.SuggestionRules[i])
                    {
                        rulesIds.Add(i + 1);
                    }
                }

                var resRules = await _context.SuggestionRules
                    .Where(r => rulesIds.Contains(r.Id))
                    .ToListAsync();

                resSuggestion.SuggestionRules = resRules;
                resSuggestion.Progress = 35;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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

        [Route("adddescription")]
        [HttpPost]
        public async Task<IActionResult> AddDescription([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.Description.Length < 50)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.Description = suggestion.Description;
                resSuggestion.Progress = 75;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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

        [Route("addstarsrating")]
        [HttpPost]
        public async Task<IActionResult> AddStarsRating([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.StarsRating < 0 ||
                    suggestion.StarsRating > 5)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.StarsRating = suggestion.StarsRating;
                resSuggestion.Progress = 50;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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

        [Route("addphotos")]
        [HttpPost]
        public async Task<IActionResult> AddPhotos(IFormFile[] uploadedFiles)
        {
            try
            {
                string suggestionId = Request.QueryString.Value;
                suggestionId = suggestionId.Substring(suggestionId.IndexOf("=") + 1);

                if (uploadedFiles is null ||
                    suggestionId is null
                    )
                {
                    return Json(new { code = 400 });
                }


                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == int.Parse(suggestionId));
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                bool res = await _fileUploader.UploadSuggestionPhotoAsync(uploadedFiles, resSuggestion);
                if (!res)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.Progress = 65;

                _context.Suggestions.Update(resSuggestion);
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

        [Route("addcontactdetails")]
        [HttpPost]
        public async Task<IActionResult> AddContactDetails([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null                                     ||
                   string.IsNullOrWhiteSpace(suggestion.ContactFirstName)  ||
                   string.IsNullOrWhiteSpace(suggestion.ContactLastName)   ||
                   string.IsNullOrWhiteSpace(suggestion.ContactPhone)      ||
                   string.IsNullOrWhiteSpace(suggestion.ContactEmail))
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                ContactDetails contactDetails = new();
                contactDetails.FirstName = suggestion.ContactFirstName;
                contactDetails.LastName = suggestion.ContactLastName;
                contactDetails.PhoneNumber = suggestion.ContactPhone;
                contactDetails.Email = suggestion.ContactEmail;
                _context.ContactDetails.Add(contactDetails);

                resSuggestion.ContactDetails = contactDetails;
                resSuggestion.Progress = 100;
                resSuggestion.SuggestionStatusId = 2;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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
