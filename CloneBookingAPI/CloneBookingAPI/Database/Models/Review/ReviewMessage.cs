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
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Incorrect length")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string PositiveText { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string NegativeText { get; set; }

        public int? ReviewId { get; set; }
        [ForeignKey("ReviewId")]
        public Models.Review.Review Review { get; set; }
    }
}
