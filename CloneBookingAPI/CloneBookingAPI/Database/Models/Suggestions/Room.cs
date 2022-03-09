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

        [Required]
        public ushort Sleeps { get; set; }

        [Required]
        public ushort RoomSize { get; set; }

        [Required]
        public bool IsSmokingAllowed { get; set; }

        [Required]
        public bool IsSuite { get; set; }

        [Display(Name = "Description")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(1000, MinimumLength = 6, ErrorMessage = "Incorrect length")]
        public string Description { get; set; }

        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PriceInUserCurrency { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PriceInUSD { get; set; }

        public List<SuggestionHighlight> Facilities { get; set; } = new();
    }
}
