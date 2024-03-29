﻿using CloneBookingAPI.Database.Models.Suggestions;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloneBookingAPI.Services.POCOs
{
    public class SuggestionPoco
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public int UserId { get; set; }
        public int BookingCategoryId { get; set; }
        public int StatusId { get; set; }
        public int StarsRating { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Description { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string BookingCategory { get; set; }
        public bool IsParkingAvailable { get; set; }

        public Address Address { get; set; }

        public List<bool> Facilities { get; set; } = new();
        public List<string> Languages { get; set; } = new();
        public List<bool> SuggestionRules { get; set; } = new();
        public List<FileModel> Images { get; set; } = new();
        public List<ApartmentPoco> Apartments { get; set; } = new();
    }
}
