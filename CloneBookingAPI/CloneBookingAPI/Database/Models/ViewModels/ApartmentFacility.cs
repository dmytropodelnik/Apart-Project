using CloneBookingAPI.Database.Models.Suggestions;
using CloneBookingAPI.Services.Database.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.ViewModels
{
    [Table("ApartmentsFacilities")]
    public class ApartmentFacility
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
    }
}
