using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Suggestions
{
    [Table("RoomTypes")]
    public class RoomType
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public ushort RoomsLeft { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Incorrect length")]
        public string Title { get; set; }

        public int SuggestionId { get; set; }
        [ForeignKey("SuggestionId")]
        public Suggestion Suggestion { get; set; }

        public List<Room> Rooms { get; set; } = new();
        public List<SuggestionHighlight> Highlights { get; set; } = new();
    }
}
