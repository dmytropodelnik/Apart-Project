using CloneBookingAPI.Database.Models.Suggestions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.ViewModels
{
    [Table("ApartmentsBookedPeriods")]
    public class ApartmentBookedPeriod
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        public int BookedPeriodId { get; set; }
        public BookedPeriod BookedPeriod { get; set; }
    }
}
