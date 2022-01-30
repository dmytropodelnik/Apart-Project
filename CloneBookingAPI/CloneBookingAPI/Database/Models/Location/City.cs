using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Location
{
    [Table("Cities")]
    public class City
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(2)]
        public string Title { get; set; }

        public int? ImageId { get; set; }
        [ForeignKey("ImageId")]
        public FileModel Image { get; set; }
    }
}
