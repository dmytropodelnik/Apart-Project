using CloneBookingAPI.Services.Database.Models.Payment;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("Bookings")]
    public class Booking
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public Payment.Payment Payment { get; set; }

        public int PriceId { get; set; }
        [ForeignKey("PriceId")]
        public Price Price { get; set; }

        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        [Display(Name = "Check-in")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Check-out")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        [Display(Name = "Promo code")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string PromoCode { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
