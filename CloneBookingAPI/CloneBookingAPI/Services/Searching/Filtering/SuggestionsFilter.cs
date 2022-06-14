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
        private List<IFilter> _applyFilters = new();
        private List<IFilter> _appliedFilters = new();
        private List<string> _appliedNameFilters = new();
        private List<string> _appliedNameCheckFilters = new();

        public SuggestionsFilter(ApartProjectDbContext context)
        {
            _context = context;
        }

        public IQueryable<Suggestion> FilterItems(IQueryable<Suggestion> suggestions, IEnumerable<FilterViewModel> filters, IEnumerable<int> amountFilteringSuggestions)
        {
            try
            {            
                if (filters is null || filters.Count() == 0)
                {
                    return null;
                }

                foreach (var filter in filters)
                {
                    if (filter.Filter.Equals("stars"))
                    {
                        _applyFilters.Add(new StarsFilter(int.Parse(filter.Value), filter.Filter));
                    }
                    else if (filter.Filter.Equals("bookingCategories"))
                    {
                        _applyFilters.Add(new BookingCategoriesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("facilities"))
                    {
                        _applyFilters.Add(new FacilitiesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("reviewScores"))
                    {
                        _applyFilters.Add(new ReviewScoresFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("prices"))
                    {
                        _applyFilters.Add(new PricesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("highlights"))
                    {
                        _applyFilters.Add(new HighlightsFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("languages"))
                    {
                        _applyFilters.Add(new LanguagesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("places"))
                    {
                        _applyFilters.Add(new PlacesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("dates"))
                    {
                        _applyFilters.Add(new DatesFilter(filter.Value, filter.Filter));
                    }
                    else if (filter.Filter.Equals("amounts"))
                    {
                        _applyFilters.Add(new AmountsFilter(filter.Value, filter.Filter));
                    }
                }

                List<Suggestion> filtered = new();

                _appliedNameFilters.Add(_applyFilters.FirstOrDefault().Filter);

                foreach (var filter in _applyFilters)
                {
                    if (_appliedNameFilters.Contains(filter.Filter))
                    {
                        var tempList = filter.FilterItems(suggestions).Except(filtered);
                        List<Suggestion> newList = new();

                        if (_appliedFilters.Any())
                        {
                            int counter = 1;
                            foreach (var item in _applyFilters)
                            {
                                if (item.Filter != filter.Filter)
                                {
                                    if (_appliedNameCheckFilters.Contains(item.Filter))
                                    {
                                        newList.AddRange(item.FilterItems(tempList));
                                        tempList = tempList.Except(newList);
                                    }
                                    else
                                    {
                                        if (_applyFilters.Skip(counter).Any(f => f.Filter == item.Filter))
                                        {
                                            newList.AddRange(item.FilterItems(tempList));
                                            tempList = tempList.Except(newList);
                                        }
                                        else
                                        {
                                            tempList = item.FilterItems(tempList);
                                        }
                                    }
                                    _appliedNameCheckFilters.Add(item.Filter);
                                }
                                counter++;
                            }
                            newList.AddRange(tempList);
                            filtered.AddRange(newList);
                            _appliedNameCheckFilters.Clear();
                        }
                        else
                        {
                            filtered.AddRange(tempList);
                        }
                    }
                    else
                    {
                        filtered = filtered.Intersect(filter.FilterItems(suggestions))
                                    .ToList();
                    }
                    if (_appliedFilters.Count > 0)
                    {
                        _appliedNameFilters.Add(filter.Filter);
                    }
                    _appliedFilters.Add(filter);
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
