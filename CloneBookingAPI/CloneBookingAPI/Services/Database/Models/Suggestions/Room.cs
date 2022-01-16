using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Suggestions
{
    [Table("Rooms")]
    public class Room
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ushort RoomAmount { get; set; }
        public ushort Sleeps { get; set; }

        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal? AmountInUserCurrency { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal AmountInUS { get; set; }

        public List<SuggestionHighlight> Highlights { get; set; } = new();
    }
}
