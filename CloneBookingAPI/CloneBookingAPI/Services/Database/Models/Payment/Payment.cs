using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Payment
{
    [Table("Payments")]
    public class Payment
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? PaymentTypeId { get; set; }
        [ForeignKey("PaymentTypeId")]
        public PaymentType PaymentType { get; set; }

        public int? CreditCardId { get; set; }
        [ForeignKey("CreditCardId")]
        public CreditCard CreditCard { get; set; }
    }
}
