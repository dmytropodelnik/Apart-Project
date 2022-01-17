using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Suggestions
{
    public class SuggestionHighlightsConfiguration : IEntityTypeConfiguration<SuggestionHighlight>
    {
        public void Configure(EntityTypeBuilder<SuggestionHighlight> builder)
        {
            builder.Property(r => r.IsSuite).HasDefaultValue(false);

            builder.HasData(
              new SuggestionHighlight[]
              {

              });
        }
    }
}
