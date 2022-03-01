using CloneBookingAPI.Services.Database.Models.Review;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Suggestions
{
    [Table("SuggestionReviewGrades")]
    public class SuggestionReviewGrade
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Grade")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Incorrect length")]
        public double Value { get; set; }

        public int ReviewCategoryId { get; set; }
        [ForeignKey("ReviewCategoryId")]
        public ReviewCategory ReviewCategory { get; set; }

        public List<Suggestion> Suggestions { get; set; } = new();
    }
}
