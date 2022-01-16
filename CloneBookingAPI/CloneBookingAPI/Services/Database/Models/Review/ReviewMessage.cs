using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CloneBookingAPI.Services.Database;

namespace CloneBookingAPI.Services.Database.Configurations.Review
{
    [Table("ReviewMessages")]
    public class ReviewMessage
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Incorrect length")]
        public string Title { get; set; }

        [Display(Name = "Message")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Incorrect length")]
        public string Text { get; set; }

        public Models.Review.Review Review { get; set; }
    }
}
