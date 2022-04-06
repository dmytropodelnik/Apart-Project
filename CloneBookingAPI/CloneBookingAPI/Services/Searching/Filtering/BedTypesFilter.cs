using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace CloneBookingAPI.Services.Searching.Filtering
{
    public class BedTypesFilter : IFilter
    {
        private readonly ApartProjectDbContext _context;

        private string _value;

        private string _filter;

        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
            }
        }

        public BedTypesFilter(string value, string filter, ApartProjectDbContext context)
        {
            _value = value;
            _filter = filter;
            _context = context;
        }

        public IQueryable<Suggestion> FilterItems()
        {
            try
            {
                var suggestions = _context.Suggestions
                    .Include(s => s.Beds)
                    .Where(s => s.Beds
                                    .Any(b => b.BedType.Title.Equals(_value)));

                return suggestions;
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
