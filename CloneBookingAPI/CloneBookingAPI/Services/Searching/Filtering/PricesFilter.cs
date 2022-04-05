using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace CloneBookingAPI.Services.Searching.Filtering
{
    public class PricesFilter : IFilter
    {
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
        public PricesFilter(string value, string filter)
        {
            _value = value;
            _filter = filter;
        }

        public IQueryable<Suggestion> FilterItems(IQueryable<Suggestion> suggestions)
        {
            try
            {
                if (suggestions is null)
                {
                    return null;
                }

                var priceRange = _value.Split("-");
                if (priceRange is null)
                {
                    return suggestions;
                }
                if (string.IsNullOrWhiteSpace(priceRange[1]))
                {
                    priceRange[1] = int.MaxValue.ToString();
                }

                //suggestions = suggestions
                //    .Where(s => s.RoomTypes
                //                    .All(t => t.Rooms
                //                                .All(r => r.PriceInUSD >= decimal.Parse(priceRange[0]) && r.PriceInUSD <= decimal.Parse(priceRange[1]))));

                suggestions = suggestions
                    .Include(s => s.Apartments)
                     .Where(s => s.Apartments
                        .All(a => a.PriceInUSD >= decimal.Parse(priceRange[0]) && a.PriceInUSD <= decimal.Parse(priceRange[1])));

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
