using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Location
{
    [Table("Addresses")]
    public class Address
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

        [Display(Name = "Zip Code")]
        [DataType(DataType.Text)]
        [MinLength(4)]
        public string ZipCode { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Incorrect length")]
        public string PhoneNumber { get; set; }

        [Column("Address")]
        [Display(Name = "Street")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(2)]
        public string AddressText { get; set; }

        public List<StayBooking> Booking { get; set; } = new();
        public List<Suggestion> Suggestions { get; set; } = new();
        public List<UserProfile.UserProfile> UserProfiles { get; set; } = new();
    }
}
