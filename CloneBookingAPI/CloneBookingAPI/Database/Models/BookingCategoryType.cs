using CloneBookingAPI.Services.Database.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models
{
    [Table("BookingCategoryTypes")]
    public class BookingCategoryType
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Incorrect length")]
        public string Type { get; set; }

        public List<BookingCategory> BookingCategories { get; set; } = new();
    }
}
