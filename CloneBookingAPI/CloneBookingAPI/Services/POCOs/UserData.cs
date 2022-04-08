using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Location;
using System;

namespace CloneBookingAPI.Services.POCOs
{
    public class UserData
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string NewEmail { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string Language { get; set; }
        public string VerificationCode { get; set; }
        public int RoleId { get; set; }
        public int GenderId { get; set; }

        public string BirthDate { get; set; }

        public string AddressText { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public Address Address { get; set; }
        public Currency Currency { get; set; }
    }
}
