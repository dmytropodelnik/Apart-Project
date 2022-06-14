using CloneBookingAPI.Services.Database.Models.UserData;
using System.Collections.Generic;

namespace CloneBookingAPI.Services.POCOs.Bookings
{
    public class StayBookingPoco : BookingPoco
    {
        public int Nights { get; set; }
        public List<string> GuestsFullNames { get; set; }
    }
}
