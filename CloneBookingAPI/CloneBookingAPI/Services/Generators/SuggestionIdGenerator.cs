using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Generators
{
    public class SuggestionIdGenerator : BaseGenerator, IGenerator
    {
        private readonly ApartProjectDbContext _context;

        public SuggestionIdGenerator(ApartProjectDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateCode()
        {
            try
            {
                var suggestions = await _context.Suggestions.ToListAsync();

                Random generator = new();
                do
                {
                    _code = generator.Next(1000000, 9999999).ToString();
                } while (suggestions.Any(s => s.UniqueCode == _code));

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
