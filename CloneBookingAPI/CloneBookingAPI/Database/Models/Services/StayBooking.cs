﻿using CloneBookingAPI.Database.Models.Services;
using CloneBookingAPI.Database.Models.Suggestions;
using CloneBookingAPI.Database.Models.UserData;
using CloneBookingAPI.Database.Models.ViewModels;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Payment;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Database.Models.UserData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("StayBookings")]
    public class StayBooking
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public bool IsForWork { get; set; }

        [Display(Name = "Special Requests")]
        [DataType(DataType.Text)]
        [StringLength(1000, MinimumLength = 6, ErrorMessage = "Incorrect length")]
        public string SpecialRequests { get; set; }

        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public Payment.Payment Payment { get; set; }

        public int PriceId { get; set; }
        [ForeignKey("PriceId")]
        public BookingPrice Price { get; set; }

        [Display(Name = "Check-in")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Check-out")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        [Display(Name = "Promo code")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string PromoCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(7, ErrorMessage = "Incorrect length")]
        public string UniqueNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(6, ErrorMessage = "Incorrect length")]
        public string PIN { get; set; }

        public int? BookingStatusId { get; set; }
        [ForeignKey("BookingStatusId")]
        public BookingStatus BookingStatus { get; set; }

        public int SuggestionId { get; set; }
        [ForeignKey("SuggestionId")]
        public Suggestion Suggestion { get; set; }

        public int CustomerInfoId { get; set; }
        [ForeignKey("CustomerInfoId")]
        public CustomerInfo CustomerInfo { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<Guest> Guests { get; set; } = new();

        public List<StayBookingsGuests> StayBookingsGuests { get; set; } = new();

    }
}
