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
                            .Include(s => s.BookingCategory)
                            .OrderBy(s => s.BookingCategory.Category.Equals("Homes") ||
                                          s.BookingCategory.Category.Equals("Apartments"));

                        break;

                    case SortState.PriceAsc:

                        suggestions = suggestions
                            .Include(s => s.Apartments)
                                .ThenInclude(a => a.RoomTypes)
                                .ThenInclude(t => t.Rooms)
                            .OrderBy(s => s.Apartments
                                .Select(t => t.RoomTypes
                                    .Select(t => t.Rooms
                                        .Select(r => r.PriceInUSD))));

                        break;

                    case SortState.PriceDesc:

                        suggestions = suggestions
                            .Include(s => s.Apartments)
                                .ThenInclude(a => a.RoomTypes)
                                .ThenInclude(t => t.Rooms)
                            .OrderBy(s => s.Apartments
                                .Select(t => t.RoomTypes
                                    .Select(t => t.Rooms
                                        .Select(r => r.PriceInUSD))));

                        break;

                    case SortState.BestReviewed:

                        suggestions = suggestions
                            .Include(s => s.SuggestionReviewGrades)
                            .OrderByDescending(s => s.SuggestionReviewGrades
                                .Average(g => g.Value));

                        break;

                    case SortState.WorstReviewed:

                        suggestions = suggestions
                            .Include(s => s.SuggestionReviewGrades)
                            .OrderBy(s => s.SuggestionReviewGrades
                                .Average(g => g.Value));

                        break;

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
                            .Include(s => s.Reviews)
                            .OrderByDescending(s => s.Reviews.Count);

                        break;

                    case SortState.BestReviewedAndLowerPrice:

                        suggestions = suggestions
                            .Include(s => s.Apartments)
                                .ThenInclude(a => a.RoomTypes)
                                .ThenInclude(t => t.Rooms)
                            .Include(s => s.SuggestionReviewGrades)
                            .OrderByDescending(s => s.SuggestionReviewGrades.Average(g => g.Value))
                            .OrderByDescending(s => s.Apartments
                                .Select(t => t.RoomTypes
                                    .Select(t => t.Rooms
                                        .Select(r => r.PriceInUSD))));

                        break;

                    case SortState.StarRatingAndLowerPrice:

                        suggestions = suggestions
                            .Include(s => s.Apartments)
                                .ThenInclude(a => a.RoomTypes)
                                .ThenInclude(t => t.Rooms)
                            .OrderByDescending(s => s.StarsRating)
                            .OrderBy(s => s.Apartments
                                .Select(t => t.RoomTypes
                                    .Select(t => t.Rooms
                                        .Select(r => r.PriceInUSD))));

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
