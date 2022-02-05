using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Suggestions
{
    public class SuggestionRuleTypesConfiguration : IEntityTypeConfiguration<SuggestionRuleType>
    {
        public void Configure(EntityTypeBuilder<SuggestionRuleType> builder)
        {
            builder.HasData(
              new SuggestionRuleType[]
              {
                  new SuggestionRuleType { Id = 1, Type = "Cancellation/prepayment", },
                  new SuggestionRuleType { Id = 2, Type = "Children & Beds", },
                  new SuggestionRuleType { Id = 3, Type = "Age restriction", },
                  new SuggestionRuleType { Id = 4, Type = "Pets", },
                  new SuggestionRuleType { Id = 5, Type = "Groups", },
                  new SuggestionRuleType { Id = 6, Type = "Cards accepted at this hotel", },
              });
        }
    }
}
