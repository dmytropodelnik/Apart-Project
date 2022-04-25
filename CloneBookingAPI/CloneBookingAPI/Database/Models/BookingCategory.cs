using CloneBookingAPI.Database.Models;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("BookingCategories")]
    public class BookingCategory
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Trip Category")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Incorrect length")]
        public string Category { get; set; }

        public int BookingCategoryTypeId { get; set; }
        [ForeignKey("BookingCategoryTypeId")]
        public BookingCategoryType BookingCategoryType { get; set; }

        public int ImageId { get; set; }
        [ForeignKey("ImageId")]
        public FileModel Image { get; set; }

        public List<StayBooking> Bookings { get; set; } = new();
        public List<Suggestion> Suggestions { get; set; } = new();
    }
}
