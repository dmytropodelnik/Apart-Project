﻿using CloneBookingAPI.Database.Models.Suggestions;
using CloneBookingAPI.Database.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Suggestions
{
    [Table("RoomTypes")]
    public class RoomType
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Incorrect length")]
        public string Title { get; set; }

        public List<Apartment> Apartments { get; set; } = new();

        public List<Room> Rooms { get; set; } = new();
        public List<SuggestionHighlight> Highlights { get; set; } = new();

        public List<ApartmentRoomType> ApartmentsRoomTypes { get; set; } = new();
    }
}
