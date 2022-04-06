using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace CloneBookingAPI.Services.Searching.Filtering
{
    public class PlacesFilter : IFilter
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

        public PlacesFilter(string value, string filter, ApartProjectDbContext context)
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
                    .Include(s => s.Address)
                    .Include(s => s.Address.Country)
                    .Include(s => s.Address.City)
                    .Where(s => _value.Contains(s.Address.AddressText) ||
                                _value.Contains(s.Address.Country.Title) ||
                                _value.Contains(s.Address.City.Title) ||
                                s.Address.Country.Title.Contains(_value) ||
                                s.Address.City.Title.Contains(_value) ||
                                s.Address.AddressText.Contains(_value));

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
