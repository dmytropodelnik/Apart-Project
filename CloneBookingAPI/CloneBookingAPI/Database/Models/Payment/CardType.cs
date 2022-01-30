using CloneBookingAPI.Services.Database.Models.Payment;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("CardTypes")]
    public class CardType
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Card Type")]
        [DataType(DataType.Text)]
        [MinLength(2)]
        public string Type { get; set; }

        public List<CreditCard> CreditCards { get; set; } = new();
    }
}
