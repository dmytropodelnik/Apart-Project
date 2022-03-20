using CloneBookingAPI.Enums;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Searching.Filtering;
using CloneBookingAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CloneBookingAPI.Controllers.Search.Filtering
{
    public class SuggestionsFilter
    {
        private readonly ApartProjectDbContext _context;
        private List<IFilter> _appliedFilters = new();

        public SuggestionsFilter(ApartProjectDbContext context)
        {
            _context = context;
        }

        public IQueryable<Suggestion> FilterItems(IQueryable<Suggestion> suggestions, IEnumerable<FilterViewModel> filters)
        {
            try
            {
                if (suggestions is null)
                {
                    return null;
                }
                
                if (filters is null)
                {
                    return suggestions;
                }

                foreach (var filter in filters)
                {
                    if (filter.Filter.Equals("stars"))
                    {
                        _appliedFilters.Add(new StarsFilter(filter.Value));
                    }
                    else if (filter.Filter.Equals("stars"))
                    {

                    }
                }

                List<Suggestion> filtered = new();

                foreach (var filter in _appliedFilters)
                {
                    filtered.AddRange(filter.FilterItems(suggestions));
                }

                return filtered
                        .Distinct()
                        .AsQueryable();
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
