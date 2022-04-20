using CloneBookingAPI.Database.Models.Suggestions;
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

        [Route("addname")]
        [HttpPost]
        public async Task<IActionResult> AddName([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    string.IsNullOrWhiteSpace(suggestion.Name) ||
                    string.IsNullOrWhiteSpace(suggestion.Login))
                {
                    return Json(new { code = 400 });
                }

                var owner = await _context.Users.FirstOrDefaultAsync(u => u.Email == suggestion.Login);
                if (owner is null)
                {
                    return Json(new { code = 400 });
                }

                Suggestion newSuggestion = new();
                newSuggestion.Name = suggestion.Name;
                newSuggestion.UserId = owner.Id;
                newSuggestion.UniqueCode = await _suggestionIdGenerator.GenerateCode();
                newSuggestion.Progress = 10;
                newSuggestion.ServiceCategoryId = 1;

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

                var region = await _context.Regions.FirstOrDefaultAsync(r => r.Title.Equals(suggestion.Address.Region.Title));
                if (region is null)
                {
                    newRegion.Title = suggestion.Address.Region.Title;
                    newRegion.City = newCity;
                    newRegion.ImageId = resCountry.ImageId;
                    _context.Regions.Add(newRegion);
                }

                var city = await _context.Cities.FirstOrDefaultAsync(c => c.Title.Equals(suggestion.Address.City.Title) &&
                                                                          c.CountryId == suggestion.Address.CountryId);
                if (city is null)
                {
                    newCity.Title = suggestion.Address.City.Title;
                    newCity.CountryId = suggestion.Address.CountryId;
                    newCity.ImageId = resCountry.ImageId;  // ???
                    _context.Cities.Add(newCity);

                    Address newAddress = new();
                    newAddress.City = newCity;
                    newAddress.AddressText = suggestion.Address.AddressText;

                    if (region is null)
                    {
                        newAddress.Region = newRegion;
                    }

                    resSuggestion.Address = newAddress;
                }
                else
                {
                    if (region is null)
                    {
                        suggestion.Address.Region = newRegion;
                    }
                    resSuggestion.Address = suggestion.Address;
                    resSuggestion.Address.CityId = city.Id;
                }
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

        [Route("addbeds")]
        [HttpPost]
        public async Task<IActionResult> AddBeds([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.Beds is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.Beds = suggestion.Beds;
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
        public async Task<IActionResult> AddPhotos(IFormFile uploadedFile)
        {
            try
            {
                string suggestionId = Request.QueryString.Value;
                suggestionId = suggestionId.Substring(suggestionId.IndexOf("=") + 1);

                if (uploadedFile is null ||
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

                bool res = await _fileUploader.UploadSuggestionPhotoAsync(uploadedFile, resSuggestion);
                if (!res)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.Progress = 60;

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

        [Route("addprice")]
        [HttpPost]
        public async Task<IActionResult> AddPrice([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null                  ||
                    suggestion.PriceInUSD < 0           ||
                    suggestion.PriceInUserCurrency < 0)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                //resSuggestion.PriceInUSD = suggestion.PriceInUSD;
                //resSuggestion.PriceInUserCurrency = suggestion.PriceInUserCurrency;
                resSuggestion.Progress = 65;

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
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.Description = suggestion.Description;
                resSuggestion.Progress = 70;

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

        [Route("addcontactdetails")]
        [HttpPost]
        public async Task<IActionResult> AddContactDetails([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null              ||
                    suggestion.ContactName is null  ||
                    suggestion.ContactPhone is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }
                ContactDetails contactDetails = new();
                contactDetails.ContactName = suggestion.ContactName;
                contactDetails.PhoneNumber = suggestion.ContactPhone;
                _context.ContactDetails.Add(contactDetails);

                resSuggestion.ContactDetails = contactDetails;
                resSuggestion.Progress = 100;

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
