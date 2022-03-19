using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CloneBookingAPI.ViewModels
{
    public class FilterViewModel
    {
      public string Filter { get; set; }
      public int Value { get; set; }
    }
}
