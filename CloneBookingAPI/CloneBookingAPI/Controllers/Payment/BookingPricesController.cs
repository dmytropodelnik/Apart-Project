using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingPricesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public BookingPricesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getprices")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingPrice>>> GetPrices()
        {
            try
            {
                var prices = await _context.BookingPrices.ToListAsync();

                return Json(new { code = 200, prices });
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

        [Route("editprice")]
        [HttpPut]
        public async Task<IActionResult> EditPrice([FromBody] BookingPrice price)
        {
            try
            {
                if (price is null)
                {
                    return Json(new { code = 400 });
                }

                var resPrice = await _context.BookingPrices.FirstOrDefaultAsync(p => p.Id == price.Id);
                if (resPrice is null)
                {
                    return Json(new { code = 400 });
                }

                _context.BookingPrices.Update(resPrice);
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
