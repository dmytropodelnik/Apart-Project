using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Review;
using CloneBookingAPI.Services.Database.Models.UserProfile;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Suggestions
{
    [Table("Suggestions")]
    public class Suggestion
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int? ServiceCategoryId { get; set; }
        [ForeignKey("ServiceCategoryId")]
        public ServiceCategory ServiceCategory { get; set; }

        public int? BookingCategoryId { get; set; }
        [ForeignKey("BookingCategoryId")]
        public BookingCategory BookingCategory { get; set; }

        public List<Review.Review> Reviews { get; set; } = new();
        public List<StayBooking> StayBookings { get; set; } = new();
        public List<ReviewCategory> AdditionalServices { get; set; } = new();
        public List<SuggestionReviewGrade> SuggestionReviewGrades { get; set; } = new();
        public List<Favorite> Favorites { get; set; } = new();
        public List<FileModel> Images { get; set; } = new();
        public List<SuggestionHighlight> Highlights { get; set; } = new();
        public List<Facility> Facilities { get; set; } = new();
        public List<RoomType> RoomTypes { get; set; } = new();
        public List<SuggestionRule> SuggestionRules { get; set; } = new();
        public List<SurroundingObject> SurroundingObjects { get; set; } = new();
    }
}
