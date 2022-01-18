﻿using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("Images")]
    public class Image
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public List<UserProfile.UserProfile> UserProfiles { get; set; } = new();
        public List<Facility> Facilities { get; set; } = new();
        public List<Suggestion> Suggestions { get; set; } = new();
        public List<SuggestionHighlight> SuggestionHighlights { get; set; } = new();
        public List<City> Cities { get; set; } = new();
        public List<Country> Countries { get; set; } = new();
        public List<Region> Regions { get; set; } = new();
        public List<District> Districts { get; set; } = new();
    }
}