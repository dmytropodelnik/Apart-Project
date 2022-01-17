using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.UserData
{
    [Table("TempUsers")]
    public class TempUser
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Enter a first name")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Enter a last name")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter an email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(320, ErrorMessage = "Incorrect length")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Incorrect length")]
        public string PhoneNumber { get; set; }

        public List<StayBooking> Bookings { get; set; } = new();
    }
}
