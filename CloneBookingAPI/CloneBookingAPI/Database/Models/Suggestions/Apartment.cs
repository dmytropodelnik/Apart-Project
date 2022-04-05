using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.Suggestions
{
    [Table("Apartments")]
    public class Apartment
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // [Required]
        [DataType(DataType.Currency)]
        public decimal PriceInUserCurrency { get; set; }
        // [Required]
        [DataType(DataType.Currency)]
        public decimal PriceInUSD { get; set; }
        // [Required]
        public int RoomsAmount { get; set; }
        // [Required]
        public int GuestsLimit { get; set; }
        // [Required]
        public int BathroomsAmount { get; set; }

        // [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int? SuggestionId { get; set; }
        [ForeignKey("SuggestionId")]
        public Suggestion Suggestion { get; set; }
        public List<BookedPeriod> BookedPeriods { get; set; } = new();

        public List<RoomType> RoomTypes { get; set; } = new();
    }
}
