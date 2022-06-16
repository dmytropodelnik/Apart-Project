using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.UserData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.ViewModels
{
    [Table("StayBookingsGuests")]
    public class StayBookingGuest
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StayBookingId { get; set; }
        public StayBooking StayBooking { get; set; }
        public int GuestId { get; set; }
        public Guest Guest { get; set; }
    }
}
