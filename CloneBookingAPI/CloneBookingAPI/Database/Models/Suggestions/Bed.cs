using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.Suggestions
{
    [Table("Beds")]
    public class Bed
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Incorrect length")]
        public string Description { get; set; }

        public int Amount { get; set; }

        public int BedTypeId { get; set; }
        [ForeignKey("BedTypeId")]
        public BedType BedType { get; set; }

        public List<Room> Rooms { get; set; } = new();
    }
}
