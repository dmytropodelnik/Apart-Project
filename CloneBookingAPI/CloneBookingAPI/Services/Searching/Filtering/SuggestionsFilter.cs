using CloneBookingAPI.Enums;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CloneBookingAPI.Controllers.Search.Filtering
{
    public class SuggestionsFilter : IFilter
    {
        private readonly ApartProjectDbContext _context;

        public SuggestionsFilter(ApartProjectDbContext context)
        {
            _context = context;
        }

        public IQueryable<Suggestion> FilterItems(IEnumerable<Suggestion> suggestions, SortState sortState)
        {
            throw new System.NotImplementedException();
        }
    }
}
