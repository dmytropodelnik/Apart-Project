using CloneBookingAPI.Enums;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Interfaces
{
    public interface ISorter
    {
        IQueryable<Suggestion> SortItems(IQueryable<Suggestion> suggestions, SortState sortOrder);
    }
}
