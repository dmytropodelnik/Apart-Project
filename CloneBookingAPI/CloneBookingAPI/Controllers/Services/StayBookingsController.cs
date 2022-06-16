using CloneBookingAPI.Database.Models.UserData;
using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Payment;
using CloneBookingAPI.Services.Database.Models.UserData;
using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.POCOs.Bookings;
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
    [Route("api/[controller]")]
    [ApiController]
    public class StayBookingsController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly BookingIdGenerator _bookingIdGenerator;
        private readonly BookingPINGenerator _bookingPINGenerator;

        public StayBookingsController(
            ApartProjectDbContext context,
            BookingIdGenerator bookingIdGenerator,
            BookingPINGenerator bookingPINGenerator)
        {
            _context = context;
            _bookingIdGenerator = bookingIdGenerator;
            _bookingPINGenerator = bookingPINGenerator;
        }

        [TypeFilter(typeof(AuthorizationFilter))]
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

        [TypeFilter(typeof(AuthorizationFilter))]
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
                    .Include(b => b.User)
                    .Include(b => b.Suggestion)
                        .ThenInclude(s => s.Images)
                    .Include(b => b.Suggestion.Address)
                    .Include(b => b.Suggestion.Address.Country)
                    .Include(b => b.Suggestion.Address.City)
                    .Include(b => b.Suggestion.BookingCategory)
                    .Include(b => b.BookingStatus)
                    .Include(b => b.CustomerInfo)
                    .Include(b => b.Guests)
                    .Include(b => b.Price)
                        .ThenInclude(p => p.Currency)
                    .Where(b => b.User.Email.Equals(email) && b.IsRevealed)
                    .ToListAsync();
                if (stayBookings is null)
                {
                    return Json(new { code = 400 });
                }

                return Json(new
                {
                    code = 200,
                    stayBookings = stayBookings
                        .Select(b => new
                        {
                            b.Id,
                            b.IsPaid,
                            b.IsRevealed,
                            b.IsForWork,
                            BookingStatus = b.BookingStatus.Status,
                            b.CheckIn,
                            b.CheckOut,
                            b.CustomerInfo,
                            Guests = b.Guests.Select(g => g.FullName),
                            b.Nights,
                            b.Price,
                            b.PromoCode,
                            b.SpecialRequests,
                            Suggestion = b.Suggestion,
                            b.UniqueNumber,
                        }),
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

        [TypeFilter(typeof(AuthorizationFilter))]
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

        [TypeFilter(typeof(AuthorizationFilter))]
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(AccessoryStayBookingFilter))]
        [Route("deletebooking")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400, message = "Incorrect stay booking id." });
                }

                var resBooking = await _context.StayBookings.FirstOrDefaultAsync(b => b.Id == id);
                if (resBooking is null)
                {
                    return Json(new { code = 400, message = "Stay bookings is not found." });
                }

                resBooking.IsRevealed = false;

                _context.StayBookings.Update(resBooking);
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

                var booking = await _context.StayBookings
                    .Include(b => b.CustomerInfo)
                    .FirstOrDefaultAsync(b => b.UniqueNumber.Equals(bookingNumber) &&
                                b.PIN.Equals(bookingPIN));
                if (booking is null)
                {
                    return Json(new { code = 400, message = "Incorrect input data." });
                }

                string owner = null;
                if (booking.UserId is not null)
                {
                    owner = booking.UserId.ToString();
                }
                else
                {
                    owner = booking.CustomerInfo.Email;
                }

                return Json(new { 
                    code = 200,
                    owner,
                    booking,
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

        [Route("addstaybooking")]
        [HttpPost]
        public async Task<IActionResult> AddStayBooking([FromBody] StayBookingPoco booking)
        {
            try
            {
                if (booking is null ||
                    booking.SuggestionId < 1 ||
                    booking.FinalPrice < 0   ||
                    booking.TotalPrice < 0   ||
                    booking.CheckIn < DateTime.UtcNow  ||
                    booking.CheckOut < DateTime.UtcNow ||
                    booking.CheckOut < booking.CheckIn ||
                    !booking.ApartmentsIds.Any()       ||
                    string.IsNullOrWhiteSpace(booking.City)          ||
                    string.IsNullOrWhiteSpace(booking.Country)       ||
                    string.IsNullOrWhiteSpace(booking.AddressText)   ||
                    string.IsNullOrWhiteSpace(booking.PhoneNumber)   ||
                    string.IsNullOrWhiteSpace(booking.CustomerEmail))
                {
                    return Json(new { code = 400, message = "Input data is incorrect or null." });
                }

                CustomerInfo newCustomerInfo = new()
                {
                    FirstName = booking.FirstName,
                    LastName = booking.LastName,
                    AddressText = booking.AddressText,
                    City = booking.City,
                    Country = booking.Country,
                    PhoneNumber = booking.PhoneNumber,
                    ZipCode = booking.ZipCode,
                    Email = booking.CustomerEmail,
                };

                BookingPrice newPrice = new()
                {
                    Discount = booking.Discount,
                    TotalPrice = booking.TotalPrice,
                    FinalPrice = booking.FinalPrice,
                    Difference = booking.Difference,
                    CurrencyId = 1,
                };

                StayBooking newStayBooking = new()
                {
                    SuggestionId = booking.SuggestionId,
                    IsForWork = booking.IsForWork,
                    CheckIn = booking.CheckIn,
                    CheckOut = booking.CheckOut,
                    CustomerInfo = newCustomerInfo,
                    BookingStatusId = 1,
                    SpecialRequests = booking.SpecialRequests,
                    PromoCode = booking.PromoCode,
                    UniqueNumber = await _bookingIdGenerator.GenerateCodeAsync(),
                    PIN = _bookingPINGenerator.GenerateCode(),
                    Price = newPrice,
                    Nights = booking.Nights,
                };

                var resApartments = await _context.Apartments
                    .Where(a => booking.ApartmentsIds.Any(i => i == a.Id))
                    .ToListAsync();

                newStayBooking.Apartments.AddRange(resApartments);

                foreach (var item in booking.GuestsFullNames)
                {
                    Guest newGuest = new()
                    {
                        FullName = item,
                    };

                    newStayBooking.Guests.Add(newGuest);
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(booking.UserEmail));
                if (user is not null)
                {
                    newStayBooking.UserId = user.Id;
                }

                var resStayBooking = _context.StayBookings.Add(newStayBooking);
                await _context.SaveChangesAsync();

                return Json(new { 
                    code = 200,
                    resStayBooking = resStayBooking.Entity,
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
