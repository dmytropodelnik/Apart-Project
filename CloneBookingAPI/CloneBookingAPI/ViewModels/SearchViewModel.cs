using CloneBookingAPI.Enums;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.Collections.Generic;

namespace CloneBookingAPI.ViewModels
{
    public class SearchViewModel
    {
        public List<Suggestion> Suggestions { get; set; }
        public SortState SortOrder { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public int TotalPages { get; set; } = 1;

        //public PageViewModel PageViewModel { get; set; }
        //public FilterViewModel FilterViewModel { get; set; }
        //public SortViewModel SortViewModel { get; set; }
    }
}
