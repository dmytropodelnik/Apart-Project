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

              });
        }
    }
}
