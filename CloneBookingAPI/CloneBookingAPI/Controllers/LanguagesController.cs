using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public LanguagesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getlanguages")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            try
            {
                var res = await _context.Languages.ToListAsync();

                return Json(new { code = 200, languages = res });
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

        [Route("addlanguage")]
        [HttpPost]
        public async Task<IActionResult> AddLanguage([FromBody] string lang)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lang))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.Languages.FirstOrDefaultAsync(l => l.Title == lang);
                if (res is null)
                {
                    Language newLanguage = new();
                    newLanguage.Title = lang;
                    _context.Languages.Add(newLanguage);
                    await _context.SaveChangesAsync();

                    return Json(new { code = 200 });
                }
                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletelangbyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteLangByName(string lang)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lang))
                {
                    return Json(new { code = 400 });
                }

                var resLang = await _context.Languages.FirstOrDefaultAsync(l => l.Title == lang);
                if (resLang is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Languages.Remove(resLang);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletelang")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLang(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resLang = await _context.Languages.FirstOrDefaultAsync(l => l.Id == id);
                if (resLang is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Languages.Remove(resLang);
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
