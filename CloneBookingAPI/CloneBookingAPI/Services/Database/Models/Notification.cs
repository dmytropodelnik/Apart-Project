using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Notification")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Incorrect length")]
        public string Value { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Incorrect length")]
        public string Image { get; set; }

        public int? EmitterId { get; set; }
        [ForeignKey("EmitterId")]
        public User EmitterUser { get; set; }

        //public int? RecipientUserId { get; set; }
        //[ForeignKey("RecipientUserId")]
        //public User RecipientUser { get; set; }
    }
}
