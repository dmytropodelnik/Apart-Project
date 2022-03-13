using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.POCOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Properties
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyEditorController : Controller
    {
        private readonly ApartProjectDbContext _context;

        private readonly IWebHostEnvironment _appEnvironment;

        public PropertyEditorController(
            ApartProjectDbContext context,
            IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // [Authorize]
        [Route("editname")]
        [HttpPut]
        public async Task<IActionResult> EditName([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    string.IsNullOrWhiteSpace(suggestion.Name) ||
                    string.IsNullOrWhiteSpace(suggestion.Login))
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.User.Email.Equals(suggestion.Login) && s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.Name = suggestion.Name;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
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

        // [Authorize]
        [Route("editlanguages")]
        [HttpPut]
        public async Task<IActionResult> EditLanguages([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null                           ||
                    suggestion.Languages is null                 ||
                    string.IsNullOrWhiteSpace(suggestion.Login))
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.User.Email.Equals(suggestion.Login) && s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resLanguages = await _context.Languages
                    .Where(f => suggestion.Languages.Contains(f.Title))
    .               ToListAsync();

                resSuggestion.Languages = resLanguages;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
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

        // [Authorize]
        [Route("addlanguages")]
        [HttpPost]
        public async Task<IActionResult> AddLanguages([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null                           ||
                    suggestion.Languages is null                 ||
                    string.IsNullOrWhiteSpace(suggestion.Login))
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.User)
                    .Include(s => s.Languages)
                    .FirstOrDefaultAsync(s => s.User.Email.Equals(suggestion.Login) && s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resLanguages = await _context.Languages
                    .Where(f => suggestion.Languages.Contains(f.Title))
                    .ToListAsync();

                resSuggestion.Languages.AddRange(resLanguages);

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
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

        // [Authorize]
        [Route("editaddress")]
        [HttpPut]
        public async Task<IActionResult> EditAddress([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.Address is null ||
                    string.IsNullOrWhiteSpace(suggestion.Login))
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.User.Email.Equals(suggestion.Login) && s.Id == suggestion.Id);
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

        // [Authorize]
        [Route("editfacilities")]
        [HttpPut]
        public async Task<IActionResult> EditFacilities([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.Facilities is null ||
                    string.IsNullOrWhiteSpace(suggestion.Login))
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.User.Email.Equals(suggestion.Login) && s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                List<int> facilitiesIds = new();
                for (int i = 0; i < suggestion.Facilities.Count; i++)
                {
                    if (suggestion.Facilities[i])
                    {
                        facilitiesIds.Add(i);
                    }
                }

                var resFacilities = await _context.Facilities
                    .Where(f => facilitiesIds.Contains(f.Id))
                    .ToListAsync();

                resSuggestion.Facilities = resFacilities;

                _context.Suggestions.Update(resSuggestion);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    code = 200,
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

        // [Authorize]
        [Route("editparking")]
        [HttpPut]
        public async Task<IActionResult> EditParking([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.User.Email.Equals(suggestion.Login) && s.Id == suggestion.Id);
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
        [Route("editrules")]
        [HttpPut]
        public async Task<IActionResult> EditRules([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null ||
                    suggestion.SuggestionRules is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.User.Email.Equals(suggestion.Login) && s.Id == suggestion.Id);
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

        // [Authorize]
        [Route("editprice")]
        [HttpPut]
        public async Task<IActionResult> EditPrice([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.User.Email.Equals(suggestion.Login) && s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                resSuggestion.PriceInUSD = suggestion.PriceInUSD;
                resSuggestion.PriceInUserCurrency = suggestion.PriceInUserCurrency;

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

        // [Authorize]
        [Route("editdescription")]
        [HttpPut]
        public async Task<IActionResult> EditDescription([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.User.Email.Equals(suggestion.Login) && s.Id == suggestion.Id);
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

        // [Authorize]
        [Route("addphotos")]
        [HttpPost]
        public async Task<IActionResult> AddPhotos(IFormFileCollection uploads)
        {
            try
            {
                string query = Request.QueryString.Value;

                string suggestionId = query.Substring(query.IndexOf("=") + 1);
                string userLogin = query.Substring(query.LastIndexOf("=") + 1);

                if (uploads is null || suggestionId is null || userLogin is null)
                {
                    return Json(new { code = 400 });
                }

                var suggestion = await _context.Suggestions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.Id == int.Parse(suggestionId) &&
                                         s.User.Email.Equals(userLogin));
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

        [Route("deleteimage")]
        [HttpDelete]
        public async Task<IActionResult> DeleteImage([FromBody] FilePoco file)
        {
            try
            {
                string query = Request.QueryString.Value;

                string suggestionId = query.Substring(query.IndexOf("=") + 1);

                if (file is null || suggestionId is null)
                {
                    return Json(new { code = 400 });
                }

                var suggestion = await _context.Suggestions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.Id == int.Parse(suggestionId) &&
                         s.User.Email.Equals(file.UserLogin));
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                foreach (var item in suggestion.Images)
                {
                    if (item.Name.Equals(file.Name) &&
                        item.Path.Equals(file.Path))
                    {
                        suggestion.Images.Remove(item);
                        _context.Files.Remove(item);

                        break;
                    }
                }

                _context.Suggestions.Update(suggestion);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
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
    }
}
