using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Suggestions
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionRuleTypesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public SuggestionRuleTypesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("gettypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuggestionRuleType>>> GetTypes()
        {
            try
            {
                var types = await _context.SuggestionRuleTypes.ToListAsync();

                return Json(new { code = 200, types });
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

        [Route("addtype")]
        [HttpPost]
        public async Task<IActionResult> AddType([FromBody] SuggestionRuleType type)
        {
            try
            {
                if (type is null)
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.SuggestionRuleTypes.FirstOrDefaultAsync(t => t.Type == type.Type);
                if (res is null)
                {
                    _context.SuggestionRuleTypes.Add(type);
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

        [Route("deletetype")]
        [HttpDelete]
        public async Task<IActionResult> DeleteType([FromBody] SuggestionRuleType type)
        {
            try
            {
                if (type is null)
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.SuggestionRuleTypes.FirstOrDefaultAsync(t => t.Type == type.Type);
                if (resType is null)
                {
                    return Json(new { code = 400 });
                }

                _context.SuggestionRuleTypes.Remove(resType);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletetype")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.SuggestionRuleTypes.FirstOrDefaultAsync(t => t.Id == id);
                if (resType is null)
                {
                    return Json(new { code = 400 });
                }

                _context.SuggestionRuleTypes.Remove(resType);
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
