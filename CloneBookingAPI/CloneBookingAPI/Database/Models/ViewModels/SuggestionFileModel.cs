using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.ViewModels
{
    [Table("SuggestionsFileModels")]
    public class SuggestionFileModel
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SuggestionId { get; set; }
        public Suggestion Suggestion { get; set; }
        public int ImageId { get; set; }
        public FileModel Image { get; set; }
    }
}
