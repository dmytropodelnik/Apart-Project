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
    public class AttractionsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public AttractionsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getbookings")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttractionBooking>>> GetBookings()
        {
            try
            {
                var bookings = await _context.AttractionBookings.ToListAsync();

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
        }

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttractionBooking>>> Search(string booking)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(booking))
                {
                    var res = await _context.CarRentalBookings.ToListAsync();

                    return Json(new { code = 200, bookings = res });
                }

                var bookings = await _context.AttractionBookings
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
        }

        [Route("addbooking")]
        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] AttractionBooking booking, IFormFile uploadedFile)
        {
            try
            {
                if (booking is null)
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.AttractionBookings.FirstOrDefaultAsync(b => b.Id == booking.Id);
                if (res is null)
                {
                    _context.AttractionBookings.Add(booking);
                    await _context.SaveChangesAsync();

                    return Json(new { code = 200 });
                }
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
        public async Task<IActionResult> EditBooking([FromBody] AttractionBooking booking)
        {
            try
            {
                if (booking is null)
                {
                    return Json(new { code = 400 });
                }

                var resBooking = await _context.AttractionBookings.FirstOrDefaultAsync(b => b.Id == booking.Id);
                if (resBooking is not null)
                {

                    _context.AttractionBookings.Update(resBooking);
                    await _context.SaveChangesAsync();

                    return Json(new { code = 200 });
                }
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
        public async Task<IActionResult> DeleteBooking([FromBody] AttractionBooking booking)
        {
            try
            {
                if (booking is null)
                {
                    return Json(new { code = 400 });
                }

                var resBooking = await _context.AttractionBookings.FirstOrDefaultAsync(b => b.Id == booking.Id);
                if (resBooking is null)
                {
                    return Json(new { code = 400 });
                }

                _context.AttractionBookings.Remove(resBooking);
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
