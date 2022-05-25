using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models
{
    [Table("ReviewMessages")]
    public class Message
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Text { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int? SuggestionId { get; set; }
        [ForeignKey("SuggestionId")]
        public Suggestion Suggestion { get; set; }
    }
}
