using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public PaymentTypesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getpaymenttypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentType>>> GetPaymentTypes()
        {
            return await _context.PaymentTypes.ToListAsync();
        }

        [Route("addpayment")]
        [HttpPost]
        public async Task<IActionResult> AddPaymentType([FromBody] PaymentType type)
        {
            if (type is null)
            {
                return Json(new { code = 400 });
            }

            var res = await _context.PaymentTypes.FirstOrDefaultAsync(t => t.Type == type.Type);
            if (res is null)
            {
                PaymentType newType = new();
                newType.Type = type.Type;
                _context.PaymentTypes.Add(newType);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            return Json(new { code = 400 });
        }

        [Route("changetypebyname")]
        [HttpPut]
        public async Task<IActionResult> ChangeTypeByName(PaymentType type)
        {
            if (type is null)
            {
                return Json(new { code = 400 });
            }

            var resType = await _context.PaymentTypes.FirstOrDefaultAsync(t => t.Type == type.Type);
            if (resType is null)
            {
                return Json(new { code = 400 });
            }
            resType.Type = type.Type;

            _context.PaymentTypes.Update(resType);
            await _context.SaveChangesAsync();

            return Json(new { code = 200 });
        }

        [Route("deletetypebyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return Json(new { code = 400 });
            }

            var resType = await _context.PaymentTypes.FirstOrDefaultAsync(t => t.Type == type);
            if (resType is null)
            {
                return Json(new { code = 400 });
            }

            _context.PaymentTypes.Remove(resType);
            await _context.SaveChangesAsync();

            return Json(new { code = 200 });
        }
    }
}
