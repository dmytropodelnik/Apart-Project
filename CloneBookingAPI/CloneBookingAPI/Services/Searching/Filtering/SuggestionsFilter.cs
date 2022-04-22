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
                if (filters is null || filters.Count() == 0)
                {
                    return null;
                }

                // filters = filters.Reverse();

                foreach (var filter in filters)
                {
                    if (filter.Filter.Equals("stars"))
                    {
                        _appliedFilters.Add(new StarsFilter(int.Parse(filter.Value), filter.Filter));
                    }
                    else if (filter.Filter.Equals("bookingCategories"))
                    {
                        _appliedFilters.Add(new BookingCategoriesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("facilities"))
                    {
                        _appliedFilters.Add(new FacilitiesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("reviewScores"))
                    {
                        _appliedFilters.Add(new ReviewScoresFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("prices"))
                    {
                        _appliedFilters.Add(new PricesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("highlights"))
                    {
                        _appliedFilters.Add(new HighlightsFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("roomTypes"))
                    {
                        _appliedFilters.Add(new RoomTypesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("languages"))
                    {
                        _appliedFilters.Add(new LanguagesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("bedTypes"))
                    {
                        _appliedFilters.Add(new BedTypesFilter(filter.Value, filter.Filter));
                    }
                }

                foreach (var filter in filters)
                {
                    if (filter.Filter.Equals("places"))
                    {
                        _appliedFilters.Add(new PlacesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("dates"))
                    {
                        _appliedFilters.Add(new DatesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("amounts"))
                    {
                        _appliedFilters.Add(new AmountsFilter(filter.Value, filter.Filter));
                    }
                }

                List<Suggestion> filtered = new();

                string previousFilter = _appliedFilters.FirstOrDefault().Filter;
                if (previousFilter is null)
                {
                    return null;
                }

                foreach (var filter in _appliedFilters)
                {
                    if (previousFilter == filter.Filter)
                    {
                        filtered.AddRange(filter.FilterItems(suggestions).ToList()
                                               .Except(filtered));
                    }
                    else
                    {
                        filtered = filtered.Intersect(filter.FilterItems(suggestions))
                                    .ToList(); 
                    }
                    previousFilter = filter.Filter;
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
