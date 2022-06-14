using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.UserData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.Review
{
    [Table("Reaction")]
    public class Reaction
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsLiked { get; set; }

        public bool IsDisliked { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int? TempUserId { get; set; }
        [ForeignKey("TempUserId")]
        public Guest TempUser { get; set; }

        public int? ReviewId { get; set; }
        [ForeignKey("ReviewId")]
        public CloneBookingAPI.Services.Database.Models.Review.Review Review { get; set; }
    }
}
