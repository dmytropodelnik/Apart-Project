using CloneBookingAPI.Enums;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CloneBookingAPI.Services.Searching.Filtering
{
    public class StarsFilter : IFilter
    {
        private int _value;
        public StarsFilter(int value)
        {
            _value = value;
        }

        public IQueryable<Suggestion> FilterItems(IQueryable<Suggestion> suggestions)
        {
            try
            {
                if (suggestions is null)
                {
                    return null;
                }

                suggestions = suggestions
                    .Where(s => s.StarsRating == _value);

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
