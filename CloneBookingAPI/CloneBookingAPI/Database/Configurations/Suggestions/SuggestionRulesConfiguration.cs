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

              });
        }
    }
}
