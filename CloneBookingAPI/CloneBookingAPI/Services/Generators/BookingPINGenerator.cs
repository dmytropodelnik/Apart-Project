using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Generators
{
    public class BookingPINGenerator : BaseGenerator, IGenerator
    {
        public string GenerateCode()
        {
            try
            {
                Random generator = new();
                _code = generator.Next(100000, 99999999).ToString();

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
