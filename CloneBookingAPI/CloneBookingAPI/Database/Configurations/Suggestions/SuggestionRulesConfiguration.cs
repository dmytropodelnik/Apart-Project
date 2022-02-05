using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Suggestions
{
    public class SuggestionRulesConfiguration : IEntityTypeConfiguration<SuggestionRule>
    {
        public void Configure(EntityTypeBuilder<SuggestionRule> builder)
        {
            builder.HasData(
              new SuggestionRule[]
              {
                  new SuggestionRule { Id = 1, Text = "Cancellation and prepayment policies vary according to accommodations type. " +
                                       "Please enter the dates of your stay and check what conditions apply to your preferred room.", 
                      SuggestionRuleTypeId = 1, },
                  new SuggestionRule { Id = 2, Text = "Children of all ages are welcome.",
                      SuggestionRuleTypeId = 2, },
                  new SuggestionRule { Id = 3, Text = "Children 12 and above are considered adults at this property.",
                      SuggestionRuleTypeId = 2, },
                  new SuggestionRule { Id = 4, Text = "To see correct prices and occupancy info, add the number and ages of children in your group to your search.",
                      SuggestionRuleTypeId = 2, },
                  new SuggestionRule { Id = 5, Text = "Additional fees are not calculated automatically in the total cost and will have to be paid for separately during your stay.",
                      SuggestionRuleTypeId = 2, },
                  new SuggestionRule { Id = 6, Text = "This property doesn't offer extra beds.",
                      SuggestionRuleTypeId = 2, },
                  new SuggestionRule { Id = 7, Text = "The maximum number of cribs allowed depends on the room you choose. Double-check the maximum capacity for the room you selected.",
                      SuggestionRuleTypeId = 2, },
                  new SuggestionRule { Id = 8, Text = "All cribs and extra beds are subject to availability.",
                      SuggestionRuleTypeId = 2, },
                  new SuggestionRule { Id = 9, Text = "The minimum age for check-in is 18.",
                      SuggestionRuleTypeId = 3, },
                  new SuggestionRule { Id = 10, Text = "Pets are not allowed.",
                      SuggestionRuleTypeId = 4, },
                  new SuggestionRule { Id = 11, Text = "When booking more than 4 rooms, different policies and additional supplements may apply.",
                      SuggestionRuleTypeId = 5, },
              });
        }
    }
}
