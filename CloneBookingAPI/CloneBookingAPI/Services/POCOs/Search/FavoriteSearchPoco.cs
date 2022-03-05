using System;

namespace CloneBookingAPI.Services.POCOs.Search
{
    public class FavoriteSearchPoco
    {
        public string UserId { get; set; }
        public int RoomAmount { get; set; }
        public int GuestsAmount { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
