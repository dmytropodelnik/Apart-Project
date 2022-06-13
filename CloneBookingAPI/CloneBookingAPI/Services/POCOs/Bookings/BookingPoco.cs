using CloneBookingAPI.Services.Database.Models.UserData;
using System;
using System.Collections.Generic;

namespace CloneBookingAPI.Services.POCOs.Bookings
{
    public class BookingPoco
    {
        public int SuggestionId { get; set; }

        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal Difference { get; set; }

        public bool IsForWork { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public string PIN { get; set; }
        public string UniqueNumber { get; set; }
        public string SpecialRequests { get; set; }
        public string PromoCode { get; set; }
        public string CustomerEmail { get; set; }
        public string UserEmail { get; set; }

        public string AddressText { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }

    }
}
