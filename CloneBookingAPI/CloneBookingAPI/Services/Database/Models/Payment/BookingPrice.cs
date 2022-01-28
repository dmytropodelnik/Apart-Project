using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Payment
{
    [Table("BookingPrices")]
    public class BookingPrice
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal AmountInUserCurrency { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal AmountInUS { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal TAX { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal ResortFee { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal DamageDeposit { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal CancellationPrice { get; set; }

        public StayBooking Booking { get; set; }
    }
}
