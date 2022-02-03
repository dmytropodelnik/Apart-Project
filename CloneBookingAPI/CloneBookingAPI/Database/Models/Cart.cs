using CloneBookingAPI.Services.Database.Models.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("Carts")]
    public class Cart
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<StayBooking> StayBookings { get; set; } = new();
        public List<FlightBooking> FlightBookings { get; set; } = new();
        public List<CarRentalBooking> CarRentalBookings { get; set; } = new();
        public List<AttractionBooking> AttractionBookings { get; set; } = new();
        public List<AirportTaxiBooking> AirportTaxiBookings { get; set; } = new();
    }
}
