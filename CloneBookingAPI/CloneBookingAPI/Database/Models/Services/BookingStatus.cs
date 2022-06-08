using CloneBookingAPI.Services.Database.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.Services
{
    [Table("BookingStatuses")]
    public class BookingStatus
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Status { get; set; }

        public List<StayBooking> Suggestions { get; set; } = new();
    }
}
