using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace CloneBookingAPI.Services.Searching.Filtering
{
    public class AmountsFilter : IFilter
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

        public AmountsFilter(string value, string filter)
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

                string adultsAmount   = _value.Substring(0, _value.IndexOf(";"));
                string childrenAmount = _value.Substring(_value.IndexOf(";") + 1, _value.LastIndexOf(";") - 2);
                string roomsAmount    = _value.Substring(_value.LastIndexOf(";") + 1);

                if (string.IsNullOrWhiteSpace(adultsAmount) ||
                    string.IsNullOrWhiteSpace(childrenAmount) ||
                    string.IsNullOrWhiteSpace(roomsAmount))
                {
                    return null;
                }

                suggestions = suggestions
                    .Where(s => s.Apartments
                                    .Any(a => a.RoomsAmount >= int.Parse(roomsAmount) &&
                                               a.GuestsLimit >= (int.Parse(adultsAmount) + int.Parse(childrenAmount))));

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
