using CloneBookingAPI.Filters;
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
    public class PaymentTypesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public PaymentTypesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getpaymenttypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentType>>> GetPaymentTypes(int page = -1, int pageSize = -1)
        {
            try
            {
                List<PaymentType> types = new();
                if (page == -1 || pageSize == -1)
                {
                    types = await _context.PaymentTypes.ToListAsync();
                }
                else
                {
                    types = await _context.PaymentTypes
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { code = 200, types });
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
        public async Task<IActionResult> AddPaymentType([FromBody] PaymentType type)
        {
            try
            {
                if (type is null)
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.PaymentTypes.FirstOrDefaultAsync(t => t.Type == type.Type);
                if (res is null)
                {
                    _context.PaymentTypes.Add(type);
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("changetypebyname")]
        [HttpPut]
        public async Task<IActionResult> ChangeTypeByName(PaymentType type)
        {
            try
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
        [Route("deletetypebyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteType(string type)
        {
            try
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
