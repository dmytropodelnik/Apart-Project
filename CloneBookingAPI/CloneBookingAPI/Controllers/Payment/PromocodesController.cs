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
    public class PromocodesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public PromocodesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getpromocodes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCode>>> GetPromoCodes()
        {
            try
            {
                var res = await _context.PromoCodes.ToListAsync();

                return Json(new { code = 200, codes = res });
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
    }
}
