﻿using CloneBookingAPI.Services.Database;
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
        public async Task<IActionResult> AddPayment([FromBody] Services.Database.Models.Payment.Payment payment)
        {
            if (payment is null)
            {
                return Json(new { code = 400 });
            }

            //var res = await _context.Payments.FirstOrDefaultAsync(c => c.Title == payment);
            //if (res is null)
            //{
            //    Country newCountry = new();
            //    newCountry.Title = payment;
            //    _context.Countries.Add(newCountry);
            //    await _context.SaveChangesAsync();

            //    return Json(new { code = 200 });
            //}
            return Json(new { code = 400 });
        }

        [Route("changepayment")]
        [HttpPut]
        public async Task<IActionResult> ChangePayment(Services.Database.Models.Payment.Payment payment)
        {
            if (payment is null)
            {
                return Json(new { code = 400 });
            }

            //var resPayment = await _context.Payments.FirstOrDefaultAsync(p => p. == country);
            //if (resPayment is null)
            //{
            //    return Json(new { code = 400 });
            //}
            //resPayment.Title = newName;

            // _context.Payments.Update(resPayment);
            // await _context.SaveChangesAsync();

            return Json(new { code = 200 });
        }

        [Route("deletepayment")]
        [HttpDelete]
        public async Task<IActionResult> DeletePayment(Services.Database.Models.Payment.Payment payment)
        {
            if (payment is null)
            {
                return Json(new { code = 400 });
            }

            //var resCountry = await _context.Payments.FirstOrDefaultAsync(c => c.Title == country);
            //if (resCountry is null)
            //{
            //    return Json(new { code = 400 });
            //}

            //_context.Payments.Remove(resCountry);
            //await _context.SaveChangesAsync();

            return Json(new { code = 200 });
        }
    }
}
