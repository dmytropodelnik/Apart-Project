using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("PromoCodes")]
    public class PromoCode
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Promo code")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string Code { get; set; }

        [Required]
        public ushort PercentDiscount { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
    }
}
