using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Payment
{
    [Table("Currencies")]
    public class CreditCard
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Cardholder's Name")]
        [Required(ErrorMessage = "Enter a last name")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect length")]
        public string Cardholder { get; set; }

        [Display(Name = "Card Number")]
        [DataType(DataType.CreditCard)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Incorrect length")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration Date")]
        [DataType(DataType.Date)]
        public DateOnly ExpirationDate { get; set; }

        [Display(Name = "CVC")]
        [Required(ErrorMessage = "Enter a CVC")]
        [DataType(DataType.Password)]
        [StringLength(3)]
        public string CVC { get; set; }

        public Payment Payment { get; set; }

        public int CardTypeId { get; set; }
        [ForeignKey("CardTypeId")]
        public CardType CardType { get; set; }
    }
}
