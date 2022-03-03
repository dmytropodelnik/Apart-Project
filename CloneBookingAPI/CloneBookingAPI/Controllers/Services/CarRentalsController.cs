using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public CarRentalsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getbookings")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarRentalBooking>>> GetBookings()
        {
            try
            {
                var bookings = await _context.CarRentalBookings.ToListAsync();

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

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarRentalBooking>>> Search(string booking)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(booking))
                {
                    var res = await _context.CarRentalBookings.ToListAsync();

                    return Json(new { code = 200, bookings = res });
                }

                var bookings = await _context.CarRentalBookings
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
        public async Task<IActionResult> AddBooking([FromBody] CarRentalBooking booking, IFormFile uploadedFile)
        {
            try
            {
                if (booking is null)
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.CarRentalBookings.FirstOrDefaultAsync(b => b.Id == booking.Id);
                if (res is null)
                {
                    _context.CarRentalBookings.Add(booking);
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
        public async Task<IActionResult> EditBooking([FromBody] CarRentalBooking booking)
        {
            try
            {
                if (booking is null)
                {
                    return Json(new { code = 400 });
                }

                var resBooking = await _context.CarRentalBookings.FirstOrDefaultAsync(b => b.Id == booking.Id);
                if (resBooking is not null)
                {

                    _context.CarRentalBookings.Update(resBooking);
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
        public async Task<IActionResult> DeleteBooking([FromBody] CarRentalBooking booking)
        {
            try
            {
                if (booking is null)
                {
                    return Json(new { code = 400 });
                }

                var resBooking = await _context.CarRentalBookings.FirstOrDefaultAsync(b => b.Id == booking.Id);
                if (resBooking is null)
                {
                    return Json(new { code = 400 });
                }

                _context.CarRentalBookings.Remove(resBooking);
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
