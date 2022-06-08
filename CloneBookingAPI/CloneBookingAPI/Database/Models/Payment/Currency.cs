using CloneBookingAPI.Services.Database.Models.Payment;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("Currencies")]
    public class Currency
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Currency")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Incorrect length")]
        public string Value { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Incorrect length")]
        public string Abbreviation { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Incorrect length")]
        public string BankCode { get; set; }

        public List<UserProfile.UserProfile> UserProfiles { get; set; } = new();
        public List<BookingPrice> BookingPrices { get; set; } = new();
    }
}
