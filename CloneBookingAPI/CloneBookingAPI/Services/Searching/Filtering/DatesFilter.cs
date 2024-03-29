﻿using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace CloneBookingAPI.Services.Searching.Filtering
{
    public class DatesFilter : IFilter
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

        public DatesFilter(string value, string filter)
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

                string dateIn = _value.Substring(0, _value.IndexOf(";"));
                string dateOut = _value.Substring(_value.IndexOf(";") + 1);

                if (string.IsNullOrWhiteSpace(dateIn) || string.IsNullOrWhiteSpace(dateOut))
                {
                    return null;
                }

                suggestions = suggestions
                    .Where(s => s.Apartments
                                    .All(a => a.BookedPeriods
                                        .All(d => (d.DateIn > Convert.ToDateTime(dateIn) &&
                                                   d.DateIn > Convert.ToDateTime(dateOut)) ||
                                                   d.DateOut < Convert.ToDateTime(dateIn) &&
                                                   d.DateOut < Convert.ToDateTime(dateOut))));

                return suggestions;
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (FormatException ex)
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
