using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardTypesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public CardTypesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("gettypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardType>>> GetTypes()
        {
            try
            {
                var res = await _context.CardTypes.ToListAsync();

                return Json(new { code = 200, types = res });
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
        public async Task<IActionResult> AddType([FromBody] CardType type)
        {
            try
            {
                if (type is null)
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.CardTypes.FirstOrDefaultAsync(t => t.Type == type.Type);
                if (res is null)
                {
                    CardType newType = new();
                    newType.Type = type.Type;
                    _context.CardTypes.Add(newType);
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
        public async Task<IActionResult> DeleteType([FromBody] CardType type)
        {
            try
            {
                if (type is null)
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.CardTypes.FirstOrDefaultAsync(t => t.Type == type.Type);
                if (resType is null)
                {
                    return Json(new { code = 400 });
                }

                _context.CardTypes.Remove(resType);
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

                var resType = await _context.CardTypes.FirstOrDefaultAsync(t => t.Id == id);
                if (resType is null)
                {
                    return Json(new { code = 400 });
                }

                _context.CardTypes.Remove(resType);
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
