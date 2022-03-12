using CloneBookingAPI.Database.Models.Suggestions;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloneBookingAPI.Services.POCOs
{
    public class SuggestionPoco
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string BookingCategory { get; set; }
        public string Login { get; set; }
        public string Description { get; set; }
        public bool IsParkingAvailable { get; set; }

        public decimal PriceInUserCurrency { get; set; }

        public decimal PriceInUSD { get; set; }

        public Address Address { get; set; }

        public List<bool> Facilities { get; set; } = new();
        public List<Language> Languages { get; set; } = new();
        public List<Bed> Beds { get; set; } = new();
        public List<SuggestionRule> SuggestionRules { get; set; } = new();
        public List<FileModel> Images { get; set; } = new();
    }
}
