using CloneBookingAPI.Enums;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
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
                            .OrderBy(s => s.BookingCategory.Category.Equals("Homes") ||
                                          s.BookingCategory.Category.Equals("Apartments"))
                            .AsQueryable();

                        break;

                    case SortState.PriceAsc:

                        suggestions = suggestions
                            .OrderBy(s => s.RoomTypes
                                .Select(t => t.Rooms
                                    .Select(r => r.PriceInUSD)))
                            .AsQueryable();

                        break;

                    case SortState.PriceDesc:

                        suggestions = suggestions
                            .OrderBy(s => s.RoomTypes
                                .Select(t => t.Rooms
                                    .Select(r => r.PriceInUSD)))
                            .AsQueryable();

                        break;

                    case SortState.BestReviewed:

                        suggestions = suggestions
                            .OrderByDescending(s => s.SuggestionReviewGrades.Average(g => g.Value))
                            .AsQueryable();

                        break;

                    case SortState.WorstReviewed:

                        suggestions = suggestions
                            .OrderBy(s => s.SuggestionReviewGrades.Average(g => g.Value))
                            .AsQueryable();

                        break;

                    case SortState.StarsAsc:

                        suggestions = suggestions
                            .OrderBy(s => s.StarsRating)
                            .AsQueryable();

                        break;

                    case SortState.StarsDesc:

                        suggestions = suggestions
                            .OrderByDescending(s => s.StarsRating)
                            .AsQueryable();

                        break;

                    case SortState.TopReviewed:

                        suggestions = suggestions
                            .OrderByDescending(s => s.Reviews.Count)
                            .AsQueryable();

                        break;

                    case SortState.BestReviewedAndLowerPrice:

                        suggestions = suggestions
                            .OrderByDescending(s => s.SuggestionReviewGrades.Average(g => g.Value))
                            .OrderByDescending(s => s.RoomTypes
                                    .Select(t => t.Rooms
                                        .Select(r => r.PriceInUSD)))
                            .AsQueryable();

                        break;

                    case SortState.StarRatingAndLowerPrice:

                        suggestions = suggestions
                            .OrderByDescending(s => s.StarsRating)
                            .OrderBy(s => s.RoomTypes
                                .Select(t => t.Rooms
                                    .Select(r => r.PriceInUSD)))
                            .AsQueryable();

                        break;

                    default:
                       
                        break;
                }

                return suggestions;
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
