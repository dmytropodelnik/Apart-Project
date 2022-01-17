using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Location
{
    [Table("Regions")]
    public class Region
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Region")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(2)]
        public string Title { get; set; }

        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public int ImageId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }
    }
}
