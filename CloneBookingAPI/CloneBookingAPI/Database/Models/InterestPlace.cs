using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models
{
    [Table("InterestPlaces")]
    public class InterestPlace
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Trip Category")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Incorrect length")]
        public string Place { get; set; }

        public int? SuggestionId { get; set; }
        [ForeignKey("SuggestionId")]
        public Suggestion Suggestion { get; set; }
    }
}
