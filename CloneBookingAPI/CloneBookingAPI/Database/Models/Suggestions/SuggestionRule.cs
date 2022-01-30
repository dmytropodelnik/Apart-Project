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

        public int? SuggestionRuleTypeId { get; set; }
        [ForeignKey("SuggestionRuleTypeId")]

        public SuggestionRuleType SuggestionRuleType { get; set; }
        public List<Suggestion> Suggestions { get; set; } = new();
    }
}
