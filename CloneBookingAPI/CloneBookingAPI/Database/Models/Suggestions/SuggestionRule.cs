using CloneBookingAPI.Database.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Suggestions
{
    [Table("SuggestionRules")]
    public class SuggestionRule
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(2)]
        public string Text { get; set; }

        public int SuggestionRuleTypeId { get; set; }
        [ForeignKey("SuggestionRuleTypeId")]
        public SuggestionRuleType SuggestionRuleType { get; set; }

        public List<Suggestion> Suggestions { get; set; } = new();

        public List<SuggestionSuggestionRule> SuggestionsSuggestionRules { get; set; } = new();
    }
}
