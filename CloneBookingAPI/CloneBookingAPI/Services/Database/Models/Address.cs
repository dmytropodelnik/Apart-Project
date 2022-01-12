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

        [Display(Name = "Street")]
        [DataType(DataType.Text)]
        [MinLength(2)]
        public string Street { get; set; }

        [Display(Name = "House")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        public string House { get; set; }
    }
}
