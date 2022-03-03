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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> Search(string currency)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currency))
                {
                    var res = await _context.Currencies.ToListAsync();

                    return Json(new { code = 200, currencies = res });
                }

                var currencies = await _context.Currencies
                    .Where(c => c.Value.Contains(currency)        ||
                                c.Abbreviation.Contains(currency) ||
                                c.BankCode.Contains(currency))
                    .ToListAsync();

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
            catch (Exception ex)
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

        [Route("editcurrency")]
        [HttpPut]
        public async Task<IActionResult> EditCurrency([FromBody] Currency currency)
        {
            try
            {
                if (currency is null)
                {
                    return Json(new { code = 400 });
                }

                var resCurrency = await _context.Currencies.FirstOrDefaultAsync(c => c.Id == currency.Id);
                if (resCurrency is null)
                {
                    return Json(new { code = 400 });
                }

                resCurrency.Value = currency.Value;
                resCurrency.Abbreviation = currency.Abbreviation;
                resCurrency.BankCode = currency.BankCode;

                _context.Currencies.Update(resCurrency);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
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

        [Route("deletecurrency")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCurrency([FromBody] Currency currency)
        {
            try
            {
                if (currency is null)
                {
                    return Json(new { code = 400 });
                }

                var resCurrency = await _context.Currencies.FirstOrDefaultAsync(c => c.Id == currency.Id);
                if (resCurrency is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Currencies.Remove(resCurrency);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
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
