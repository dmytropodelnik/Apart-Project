using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("Facilities")]
    public class Facility
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Incorrect length")]
        public string Text { get; set; }

        public int? ImageId { get; set; }
        [ForeignKey("ImageId")]
        public FileModel Image { get; set; }

        public int FacilityTypeId { get; set; }
        [ForeignKey("FacilityTypeId")]
        public FacilityType FacilityType { get; set; }

        public int? SuggestionId { get; set; }
        [ForeignKey("SuggestionId")]
        public Suggestion Suggestion { get; set; }
    }
}
