using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.POCOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Suggestions
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListNewPropertyController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public ListNewPropertyController(ApartProjectDbContext context)
        {
            _context = context;
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

                var resSuggestion = _context.Suggestions.Add(newSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Entity.Id,
                });
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
                    string.IsNullOrWhiteSpace(suggestion.Login) ||
                    string.IsNullOrWhiteSpace(suggestion.Address.Country.Title) ||
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

                resSuggestion.Address = suggestion.Address;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.Beds = suggestion.Beds;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }



                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
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
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.Facilities = suggestion.Facilities;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
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
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.PriceInUS = suggestion.PriceInUS;
                resSuggestion.PriceInUserCurrency = suggestion.PriceInUserCurrency;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
                    savedSuggestionId = resSuggestion.Id,
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }
    }
}
