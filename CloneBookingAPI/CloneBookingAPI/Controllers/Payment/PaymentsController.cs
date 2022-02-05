using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Location;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public PaymentsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getpayments")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Services.Database.Models.Payment.Payment>>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        [Route("addpayment")]
        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] string payment, IFormFile uploadedFile)
        {
            if (string.IsNullOrWhiteSpace(payment))
            {
                return Json(new { code = 400 });
            }

            var res = await _context.Countries.FirstOrDefaultAsync(c => c.Title == payment);
            if (res is null)
            {
                Country newCountry = new();
                newCountry.Title = payment;
                _context.Countries.Add(newCountry);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            return Json(new { code = 400 });
        }

        [Route("changepaymentbyname")]
        [HttpPut]
        public async Task<IActionResult> ChangeCountryByName(string country, string newName)
        {
            if (string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(newName))
            {
                return Json(new { code = 400 });
            }

            var resCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Title == country);
            if (resCountry is null)
            {
                return Json(new { code = 400 });
            }
            resCountry.Title = newName;

            _context.Countries.Update(resCountry);
            await _context.SaveChangesAsync();

            return Json(new { code = 200 });
        }

        [Route("deletecountrybyname")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                return Json(new { code = 400 });
            }

            var resCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Title == country);
            if (resCountry is null)
            {
                return Json(new { code = 400 });
            }

            _context.Countries.Remove(resCountry);
            await _context.SaveChangesAsync();

            return Json(new { code = 200 });
        }
    }
}
