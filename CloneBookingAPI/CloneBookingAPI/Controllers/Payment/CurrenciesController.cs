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
    public class CurrenciesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public CurrenciesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getcurrencies")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            try
            {
                var currencies = await _context.Currencies.ToListAsync();

                return Json(new { code = 200, currencies });
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

        [Route("addcurrency")]
        [HttpPost]
        public async Task<IActionResult> AddCurrency([FromBody] Currency currency)
        {
            try
            {
                if (currency is null)
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.Currencies.FirstOrDefaultAsync(c => c.Value == currency.Value);
                if (res is null)
                {
                    _context.Currencies.Add(currency);
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

        [Route("deletecurrencybynumber")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCurrencyByName(string currency)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currency))
                {
                    return Json(new { code = 400 });
                }

                var resCurrency = await _context.Currencies.FirstOrDefaultAsync(c => c.Value == currency);
                if (resCurrency is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Currencies.Remove(resCurrency);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletecurrency")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resCurrency = await _context.Currencies.FirstOrDefaultAsync(c => c.Id == id);
                if (resCurrency is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Currencies.Remove(resCurrency);
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
