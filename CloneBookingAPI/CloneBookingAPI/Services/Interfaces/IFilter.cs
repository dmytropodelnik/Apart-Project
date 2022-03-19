using CloneBookingAPI.Enums;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace CloneBookingAPI.Services.Interfaces
{
    public interface IFilter
    {
        IQueryable<Suggestion> FilterItems(IEnumerable<Suggestion> suggestions, IEnumerable<FilterViewModel> filters);
    }
}
