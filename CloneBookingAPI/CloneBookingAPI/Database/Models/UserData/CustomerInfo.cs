using CloneBookingAPI.Services.Database.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.UserData
{
    [Table("CustomerInfos")]
    public class CustomerInfo
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string AddressText { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }

        public List<StayBooking> StayBookings { get; set; } = new();
        public List<CloneBookingAPI.Services.Database.Models.Review.Review> Reviews { get; set; } = new();
    }
}
