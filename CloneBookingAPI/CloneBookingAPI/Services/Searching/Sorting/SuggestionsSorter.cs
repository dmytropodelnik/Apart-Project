using CloneBookingAPI.Enums;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Search.Sorting
{
    public class SuggestionsSorter : ISorter
    {
        private readonly ApartProjectDbContext _context;

        public SuggestionsSorter(ApartProjectDbContext context)
        {
            _context = context;
        }

        public IQueryable<Suggestion> SortItems(IQueryable<Suggestion> suggestions, SortState sortOrder)
        {
            try
            {
                if (suggestions is null)
                {
                    return null;
                }

                // sorting
                switch (sortOrder)
                {
                    case SortState.HomeAndApartmentsFirst:

                        suggestions = suggestions
                            .OrderBy(s => s.BookingCategory.BookingCategoryTypeId);

                        break;

                    case SortState.PriceAsc:

                        suggestions = suggestions
                            .OrderBy(s => s.Apartments
                                .Min(a => a.PriceInUSD));

                        break;

                    case SortState.PriceDesc:

                        suggestions = suggestions
                            .OrderByDescending(s => s.Apartments
                                .Min(a => a.PriceInUSD));

                        break;

                    case SortState.BestReviewed:
                        {
                            var tempSuggestions = suggestions
                                .Where(s => s.Reviews.Count > 0)
                                .OrderByDescending(s => s.Reviews
                                    .Average(r => r.Grades.Average(g => g.Value)))
                                .ToList();

                            tempSuggestions.AddRange(suggestions.Except(tempSuggestions));

                            suggestions = tempSuggestions.AsQueryable();

                            break;
                        }
                    case SortState.WorstReviewed:
                        {
                            var tempSuggestions = suggestions
                                .Where(s => s.Reviews.Count > 0)
                                .OrderBy(s => s.Reviews
                                    .Average(g => g.Grades.Average(g => g.Value)))
                                .ToList();

                            tempSuggestions.InsertRange(0, suggestions.Except(tempSuggestions));

                            suggestions = tempSuggestions.AsQueryable();

                            break;
                        }
                    case SortState.StarsAsc:

                        suggestions = suggestions
                            .OrderBy(s => s.StarsRating);

                        break;

                    case SortState.StarsDesc:

                        suggestions = suggestions
                            .OrderByDescending(s => s.StarsRating);

                        break;

                    case SortState.TopReviewed:

                        suggestions = suggestions
                            .OrderByDescending(s => s.Reviews.Count);

                        break;

                    case SortState.BestReviewedAndLowerPrice:
                        {
                            var tempSuggestions = suggestions
                                .Where(s => s.Reviews.Count > 0)
                                .OrderBy(s => s.Apartments
                                    .Min(a => a.PriceInUSD))
                                .OrderByDescending(s => s.Reviews.Average(g => g.Grades.Average(g => g.Value)))
                                .ToList();

                            tempSuggestions.AddRange(suggestions.Except(tempSuggestions));

                            suggestions = tempSuggestions.AsQueryable();

                            break;
                        }
                    case SortState.StarRatingAndLowerPrice:

                        suggestions = suggestions
                            .OrderBy(s => s.Apartments
                                .Min(a => a.PriceInUSD))
                            .OrderByDescending(s => s.StarsRating);

                        break;

                    default:
                       
                        break;
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
