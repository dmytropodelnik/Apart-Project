using System;
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

        [Required]
        public string Code { get; set; }

        [Required]
        public int PercentDiscount { get; set; }
        
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime GeneratingDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
