using CloneBookingAPI.Enums;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.Collections.Generic;

namespace CloneBookingAPI.ViewModels
{
    public class SearchViewModel
    {
        public List<Suggestion> Suggestions { get; set; }
        public List<FilterViewModel> Filters { get; set; }

        public Address Address { get; set; }

        public SortState SortOrder { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public int TotalPages { get; set; } = 1;

        public string DateIn { get; set; }
        public string DateOut { get; set; }

        public int SearchAdultsAmount { get; set; }
        public int SearchChildrenAmount { get; set; }
        public int SearchRoomsAmount { get; set; }
        public int GuestsAmount { get; set; }
    }
}
