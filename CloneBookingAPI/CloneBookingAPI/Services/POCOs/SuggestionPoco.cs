using CloneBookingAPI.Services.Database.Models.Location;

namespace CloneBookingAPI.Services.POCOs
{
    public class SuggestionPoco
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public Address Address { get; set; }
    }
}
