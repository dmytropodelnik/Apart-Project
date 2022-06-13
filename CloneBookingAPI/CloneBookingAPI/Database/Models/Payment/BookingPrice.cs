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
        public decimal Discount { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public decimal FinalPrice { get; set; }
        [Required]
        public decimal Difference { get; set; }

        public decimal TAX { get; set; }

        [DataType(DataType.Currency)]
        public decimal ResortFee { get; set; }

        [DataType(DataType.Currency)]
        public decimal DamageDeposit { get; set; }

        [DataType(DataType.Currency)]
        public decimal CancellationPrice { get; set; }

        public int? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public StayBooking Booking { get; set; }
    }
}
