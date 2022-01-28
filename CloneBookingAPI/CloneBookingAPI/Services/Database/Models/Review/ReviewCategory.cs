using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Review
{
    [Table("ReviewCategories")]
    public class ReviewCategory
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Review Category")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Incorrect length")]
        public string Category { get; set; }

        public int? SuggestionId { get; set; }
        [ForeignKey("SuggestionId")]
        public Suggestion Suggestion { get; set; }

        public List<SuggestionReviewGrade> SuggestionReviewGrades { get; set; } = new();
    }
}
