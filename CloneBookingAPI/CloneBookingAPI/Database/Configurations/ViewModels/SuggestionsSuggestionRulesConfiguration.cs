using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class SuggestionsSuggestionRulesConfiguration : IEntityTypeConfiguration<SuggestionSuggestionRule>
    {
        public void Configure(EntityTypeBuilder<SuggestionSuggestionRule> builder)
        {
            builder.HasData(
              new SuggestionSuggestionRule[]
              {
                    
              });
        }
    }
}
