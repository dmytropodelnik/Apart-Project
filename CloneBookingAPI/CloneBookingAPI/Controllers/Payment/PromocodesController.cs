using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Interfaces;
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
    public class PromoCodesController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly IPromoGenerator _promoGenerator;

        private const int _WIDTHRANGE = 4;
        private const int _AMOUNTRANGE = 4;

        public PromoCodesController(
            ApartProjectDbContext context,
            PromoCodeGenerator promoGenerator)
        {
            _context = context;
            _promoGenerator = promoGenerator;
        }

        [Route("getcodes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCode>>> GetPromoCodes(int page = -1, int pageSize = -1)
        {
            try
            {
                List<PromoCode> codes = new();
                if (page == -1 || pageSize == -1)
                {
                    codes = await _context.PromoCodes.ToListAsync();
                }
                else
                {
                    codes = await _context.PromoCodes
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { code = 200, codes });
            }
            catch (ArgumentNullException ex)
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

        [Route("getlastcodes")]
        [HttpGet]
        public async Task<ActionResult> GetLastCodes()
        {
            try
            {
                var codes = await _context.PromoCodes
                    .Where(c => c.GeneratingDate.Date == DateTime.UtcNow.Date)
                    .ToListAsync();

                return Json(new { code = 200, codes });
            }
            catch (ArgumentNullException ex)
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

        [Route("isexists")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCode>>> IsExists(string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    var res = await _context.PromoCodes.ToListAsync();

                    return Json(new { code = 200, codes = res });
                }

                var codes = await _context.PromoCodes
                    .Where(c => c.Code.Contains(code)   ||
                                c.PercentDiscount.ToString().Contains(code))
                    .ToListAsync();

                return Json(new { code = 200, codes });
            }
            catch (ArgumentNullException ex)
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

        [Route("generatecode")]
        [HttpPost]
        public async Task<IActionResult> GenerateCode(int discount, int count, string expirationDate)
        {
            try
            {
                if (discount < 1 || discount > 100 ||
                    count < 1 || expirationDate is null)
                {
                    return Json(new { code = 400 });
                }

                List<PromoCode> promoCodes = new();

                for (int i = 0; i < count; i++)
                {
                    string promocode = default;

                    do
                    {
                        promocode = _promoGenerator.GenerateCode(_WIDTHRANGE, _AMOUNTRANGE);
                    } while (await _context.PromoCodes.AnyAsync(c => c.Code.Equals(promocode) && c.IsActive));

                    if (promocode is null)
                    {
                        return Json(new { code = 400 });
                    }

                    PromoCode newPromoCode = new();
                    newPromoCode.Code = promocode;
                    newPromoCode.IsActive = true;
                    newPromoCode.PercentDiscount = discount;
                    newPromoCode.GeneratingDate = DateTime.Now.ToUniversalTime();

                    DateTime expiresDate = Convert.ToDateTime(expirationDate);
                    expiresDate = expiresDate.AddDays(1);
                    
                    newPromoCode.ExpirationDate = expiresDate.ToUniversalTime();

                    _context.PromoCodes.Add(newPromoCode);
                    promoCodes.Add(newPromoCode);
                }

                await _context.SaveChangesAsync();

                return Json(new { 
                    code = 200,
                    promoCodes, 
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
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (ArgumentNullException ex)
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
