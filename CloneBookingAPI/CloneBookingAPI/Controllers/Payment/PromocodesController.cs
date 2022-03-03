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
    public class PromocodesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public PromocodesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getcodes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCode>>> GetPromoCodes()
        {
            try
            {
                var codes = await _context.PromoCodes.ToListAsync();

                return Json(new { code = 200, codes });
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
        public async Task<ActionResult<IEnumerable<PromoCode>>> Search(string code)
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
