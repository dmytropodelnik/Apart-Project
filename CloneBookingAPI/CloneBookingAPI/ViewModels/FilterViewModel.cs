using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CloneBookingAPI.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Suggestion> suggestions, int? suggestion, string title)
        {

        }
        public SelectList Companies { get; private set; } // список компаний
        public int? SelectedCompany { get; private set; }   // выбранная компания
        public string SelectedName { get; private set; }    // введенное имя
    }
}
