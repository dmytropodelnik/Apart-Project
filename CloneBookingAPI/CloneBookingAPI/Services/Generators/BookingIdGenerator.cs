using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Generators
{
    public class BookingIdGenerator : BaseGenerator, IGenerator
    {
        private readonly ApartProjectDbContext _context;

        public BookingIdGenerator(ApartProjectDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateCodeAsync()
        {
            try
            {
                var bookings = await _context.StayBookings.ToListAsync();

                Random generator = new();
                do
                {
                    _code = generator.Next(10000000, 99999999).ToString();
                } while (bookings.Any(b => b.UniqueNumber == _code));

                return _code;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
