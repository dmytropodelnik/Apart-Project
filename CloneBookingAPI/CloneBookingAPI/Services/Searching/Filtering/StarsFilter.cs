using CloneBookingAPI.Enums;
using CloneBookingAPI.Services.Database;
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
        private string _filter;

        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
            }
        }

        public StarsFilter(int value, string filter)
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
