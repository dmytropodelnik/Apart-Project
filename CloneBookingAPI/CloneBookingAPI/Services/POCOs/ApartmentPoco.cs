namespace CloneBookingAPI.Services.POCOs
{
    public class ApartmentPoco
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? PriceInUserCurrency { get; set; }
        public decimal PriceInUSD { get; set; }
        public int RoomsAmount { get; set; }
        public int GuestsLimit { get; set; }
        public int BathroomsAmount { get; set; }
    }
}
