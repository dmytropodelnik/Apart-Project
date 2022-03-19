using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.Collections.Generic;
using System.Linq;

namespace CloneBookingAPI.Services.Interfaces
{
    public interface IPaginator
    {
        IQueryable<Suggestion> SelectItems(IQueryable<Suggestion> suggestions, int page, int pageSize);
    }
}
