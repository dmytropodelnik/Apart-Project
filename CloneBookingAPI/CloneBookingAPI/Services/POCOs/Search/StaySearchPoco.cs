using CloneBookingAPI.Services.Database.Models.Location;

namespace CloneBookingAPI.Services.POCOs.Search
{
    public class StaySearchPoco
    {
        public int Page { get; set; }
        public Address Address { get; set; }
        public string Date { get; set; }
        public uint AdultsAmount { get; set; }
        public uint ChildrenAmount { get; set; }
        public uint RoomsAmount { get; set; }
        public uint GuestsAmount { get; set; }
    }
}
