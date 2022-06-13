using CloneBookingAPI.Database.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.UserData
{
    [Table("Guests")]
    public class Guest
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FullName { get; set; }

        public List<StayBooking> StayBookings { get; set; } = new();

        public List<StayBookingsGuests> StayBookingsGuests { get; set; } = new();
    }
}
