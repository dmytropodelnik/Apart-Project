using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Country")]
        [DataType(DataType.Text)]
        [MinLength(2)]
        public string Country { get; set; }

        [Display(Name = "City")]
        [DataType(DataType.Text)]
        [MinLength(2)]
        public string City { get; set; }

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
        [DataType(DataType.Text)]
        [MinLength(2)]
        public string AddressText { get; set; }
    }
}
