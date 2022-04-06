using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace CloneBookingAPI.Services.Searching.Filtering
{
    public class HighlightsFilter : IFilter
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
        public HighlightsFilter(string value, string filter, ApartProjectDbContext context)
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
                    .Include(s => s.Highlights)
                    .Where(s => s.Highlights
                                    .Any(h => h.Text.Equals(_value)));

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
