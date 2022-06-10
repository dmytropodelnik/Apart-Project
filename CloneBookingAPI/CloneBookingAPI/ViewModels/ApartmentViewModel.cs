namespace CloneBookingAPI.ViewModels
{
    public class ApartmentViewModel
    {
        public string DateIn { get; set; }
        public string DateOut { get; set; }

        public int SearchRoomsAmount { get; set; }
        public int GuestsAmount { get; set; }

        public int SuggestionId { get; set; }
    }
}
