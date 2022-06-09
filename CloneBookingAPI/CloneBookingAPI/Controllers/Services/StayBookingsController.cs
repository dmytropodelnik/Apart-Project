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

namespace CloneBookingAPI.Controllers.Services
{
    [TypeFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class StayBookingsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public StayBookingsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("getbookings")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StayBooking>>> GetBookings()
        {
            try
            {
                var bookings = await _context.StayBookings.ToListAsync();

                return Json(new { code = 200, bookings });
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

        [Route("getuserbookings")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StayBooking>>> GetUserBookings(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                var stayBookings = await _context.StayBookings
                    .Include(r => r.User)
                    .Where(r => r.User.Email.Equals(email))
                    .ToListAsync();
                if (stayBookings is null)
                {
                    return Json(new { code = 400 });
                }

                return Json(new
                {
                    code = 200,
                    stayBookings,
                });
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

        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StayBooking>>> Search(string booking)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(booking))
                {
                    var res = await _context.StayBookings.ToListAsync();

                    return Json(new { code = 200, bookings = res });
                }

                var bookings = await _context.StayBookings
                    //.Where(c => c.Title.Contains(country))
                    .ToListAsync();

                return Json(new { code = 200, bookings });
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

        [Route("addbooking")]
        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] StayBooking booking, IFormFile uploadedFile)
        {
            try
            {
                if (booking is null)
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.StayBookings.FirstOrDefaultAsync(b => b.Id == booking.Id);
                if (res is null)
                {
                    _context.StayBookings.Add(booking);
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

        [Route("editbooking")]
        [HttpPut]
        public async Task<IActionResult> EditBooking([FromBody] StayBooking booking)
        {
            try
            {
                if (booking is null)
                {
                    return Json(new { code = 400 });
                }

                var resBooking = await _context.StayBookings.FirstOrDefaultAsync(b => b.Id == booking.Id);
                if (resBooking is not null)
                {

                    _context.StayBookings.Update(resBooking);
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

        [Route("deletebooking")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBooking([FromBody] StayBooking booking)
        {
            try
            {
                if (booking is null)
                {
                    return Json(new { code = 400 });
                }

                var resBooking = await _context.StayBookings.FirstOrDefaultAsync(b => b.Id == booking.Id);
                if (resBooking is null)
                {
                    return Json(new { code = 400 });
                }

                _context.StayBookings.Remove(resBooking);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
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

        [Route("verifyowner")]
        [HttpGet]
        public async Task<IActionResult> VerifyOwner(string bookingNumber, string bookingPIN)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(bookingNumber) ||
                    string.IsNullOrWhiteSpace(bookingPIN))
                {
                    return Json(new { code = 400, message = "Input data is null." });
                }

                var res = await _context.StayBookings
                    .FirstOrDefaultAsync(b => b.UniqueNumber.Equals(bookingNumber) &&
                                b.PIN.Equals(bookingPIN));
                if (res is null)
                {
                    return Json(new { code = 400, message = "Incorrect input data." });
                }

                return Json(new { 
                    code = 200,
                    bookingId = res.Id,
                    message = "Owner is verified.",
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
