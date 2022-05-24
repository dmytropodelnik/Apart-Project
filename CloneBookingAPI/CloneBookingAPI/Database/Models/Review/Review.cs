using CloneBookingAPI.Database.Models.Review;
using CloneBookingAPI.Services.Database.Configurations.Review;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Review
{
    [Table("Reviews")]
    public class Review
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int SuggestionId { get; set; }
        [ForeignKey("SuggestionId")]
        public Suggestion Suggestion { get; set; }

        //public int? StayBookingId { get; set; }
        //[ForeignKey("StayBookingId")]
        //public StayBooking StayBooking { get; set; }

        [Required]
        public DateTime ReviewedDate { get; set; }

        public ReviewMessage ReviewMessage { get; set; }
        public SuggestionReviewGrade Grade { get; set; }

        public List<Reaction> Reactions { get; set; } = new();
    }
}
