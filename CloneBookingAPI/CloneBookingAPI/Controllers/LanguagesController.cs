using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                var languages = await _context.Languages.ToListAsync();

                return Json(new { code = 200, languages });
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

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> Search(string lang)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lang))
                {
                    var res = await _context.Languages.ToListAsync();

                    return Json(new { code = 200, languages = res });
                }

                var languages = await _context.Languages
                    .Where(l => l.Title.Contains(lang))
                    .ToListAsync();

                return Json(new { code = 200, languages });
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
        public async Task<IActionResult> AddLanguage([FromBody] Language lang)
        {
            try
            {
                if (lang is null || string.IsNullOrWhiteSpace(lang.Title))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.Languages.FirstOrDefaultAsync(l => l.Title == lang.Title);
                if (res is null)
                {
                    _context.Languages.Add(lang);
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

        [Route("editlanguage")]
        [HttpPut]
        public async Task<IActionResult> EditLanguage([FromBody] Language lang)
        {
            try
            {
                if (lang is null || string.IsNullOrWhiteSpace(lang.Title))
                {
                    return Json(new { code = 400 });
                }

                var resLang = await _context.Languages.FirstOrDefaultAsync(l => l.Id == lang.Id);
                if (resLang is not null)
                {
                    resLang.Title = lang.Title;

                    _context.Languages.Update(resLang);
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
        public async Task<IActionResult> DeleteLang([FromBody] Language lang)
        {
            try
            {
                if (lang is null || lang.Id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resLang = await _context.Languages.FirstOrDefaultAsync(l => l.Id == lang.Id);
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
