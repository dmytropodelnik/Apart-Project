using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.POCOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Suggestions
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListNewPropertyController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly SuggestionIdGenerator _suggestionIdGenerator;

        public ListNewPropertyController(
            ApartProjectDbContext context,
            SuggestionIdGenerator suggestionIdGenerator)
        {
            _context = context;
            _suggestionIdGenerator = suggestionIdGenerator;
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

                var city = await _context.Cities.FirstOrDefaultAsync(c => c.Title.Equals(suggestion.Address.City.Title) &&
                                                                          c.CountryId == suggestion.Address.CountryId);
                if (city is null)
                {
                    City newCity = new();
                    newCity.Title = suggestion.Address.City.Title;
                    newCity.CountryId = suggestion.Address.CountryId;
                    newCity.ImageId = resCountry.ImageId;  // ???
                    _context.Cities.Add(newCity);

                    Address newAddress = new();
                    newAddress.City = newCity;
                    newAddress.AddressText = suggestion.Address.AddressText;

                    resSuggestion.Address = newAddress;
                }
                else
                {
                    resSuggestion.Address = suggestion.Address;
                    resSuggestion.Address.CityId = city.Id;
                }
                resSuggestion.Progress = 20;

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

        [Route("addlanguages")]
        [HttpPost]
        public async Task<IActionResult> AddLanguages([FromBody] SuggestionPoco suggestion)
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

                resSuggestion.Languages = suggestion.Languages;
                resSuggestion.Progress = 60;

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

        [Route("addrules")]
        [HttpPost]
        public async Task<IActionResult> AddRules([FromBody] SuggestionPoco suggestion)
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

                resSuggestion.SuggestionRules = suggestion.SuggestionRules;
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

        [Route("addphotos")]
        [HttpPost]
        public async Task<IActionResult> AddPhotos(IFormFileCollection uploads)
        {
            try
            {
                string suggestionId = Request.QueryString.Value;
                suggestionId = suggestionId.Substring(suggestionId.IndexOf("=") + 1);

                if (uploads is null || suggestionId is null)
                {
                    return Json(new { code = 400 });
                }

                var suggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == int.Parse(suggestionId));
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                return RedirectToAction("UploadFiles", "FileUploader", new { uploads, suggestion });
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

        [Route("addprice")]
        [HttpPost]
        public async Task<IActionResult> AddPrice([FromBody] SuggestionPoco suggestion)
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

                resSuggestion.PriceInUSD = suggestion.PriceInUSD;
                resSuggestion.PriceInUserCurrency = suggestion.PriceInUserCurrency;
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
