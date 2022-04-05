using CloneBookingAPI.Database.Models.Suggestions;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.ViewModels
{
    [Table("ApartmentsRoomTypes")]
    public class ApartmentRoomType
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
    }
}
