using CloneBookingAPI.Services.Database.Models.Review;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.Review
{
    [Table("SuggestionReviewGrades")]
    public class SuggestionReviewGrade
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public double Value { get; set; }

        public int ReviewCategoryId { get; set; }
        [ForeignKey("ReviewCategoryId")]
        public ReviewCategory ReviewCategory { get; set; }

        public int? ReviewId { get; set; }
        [ForeignKey("ReviewId")]
        public CloneBookingAPI.Services.Database.Models.Review.Review Review { get; set; }
    }
}
