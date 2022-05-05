using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Location;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<CloneBookingAPI.Services.Database.Models.Payment.Payment>>> 
            GetPayments(int page = -1, int pageSize = -1)
        {
            try
            {
                List<CloneBookingAPI.Services.Database.Models.Payment.Payment> payments = new();
                if (page == -1 || pageSize == -1)
                {
                    payments = await _context.Payments.ToListAsync();
                }
                else
                {
                    payments = await _context.Payments
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { code = 200, payments });
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("addpayment")]
        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] CloneBookingAPI.Services.Database.Models.Payment.Payment payment)
        {
            try
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("changepayment")]
        [HttpPut]
        public async Task<IActionResult> ChangePayment(CloneBookingAPI.Services.Database.Models.Payment.Payment payment)
        {
            try
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("deletepayment")]
        [HttpDelete]
        public async Task<IActionResult> DeletePayment(CloneBookingAPI.Services.Database.Models.Payment.Payment payment)
        {
            try
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
