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

        public IQueryable<Suggestion> FilterItems(IEnumerable<Suggestion> suggestions, IEnumerable<FilterViewModel> filters)
        {
            try
            {
                if (suggestions is null)
                {
                    return null;
                }
                if (filters is null)
                {
                    return suggestions.AsQueryable();
                }

                foreach (var filter in filters)
                {
                    suggestions = suggestions
                        .Where(s => s.StarsRating > _value);
                }

                return suggestions.AsQueryable();
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (InvalidOperationException ex)
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
