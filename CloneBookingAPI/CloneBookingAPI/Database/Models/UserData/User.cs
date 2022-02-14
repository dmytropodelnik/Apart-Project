using CloneBookingAPI.Services.Database.Models.Services;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Database.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("Users")]
    public class User
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string Title { get; set; }

        [Display(Name = "First name")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string LastName { get; set; }

        [Display(Name = "Display name")]
        [DataType(DataType.Text)]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string DisplayName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter an email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(320, ErrorMessage = "Incorrect length")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Incorrect length")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string PasswordHash { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string SaltHash { get; set; }

        // !!!!! CASCADE 
        // public int? FavoriteId { get; set; }
        // [ForeignKey("FavoriteId")]
        public Favorite Favorite { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public Cart Cart { get; set; }

        // REDUNDANT
        // public int ProfileId { get; set; }
        public UserProfile.UserProfile Profile { get; set; }

        public List<Review.Review> Reviews { get; set; } = new();
        public List<Notification> Notifications { get; set; } = new();
        public List<Suggestion> Suggestions { get; set; } = new();
        public List<StayBooking> StayBookings { get; set; } = new();
        public List<FlightBooking> FlightBookings { get; set; } = new();
        public List<CarRentalBooking> CarRentalBookings { get; set; } = new();
        public List<AttractionBooking> AttractionBookings { get; set; } = new();
        public List<AirportTaxiBooking> AirportTaxiBookings { get; set; } = new();
    }
}
