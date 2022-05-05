using CloneBookingAPI.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.Suggestions
{
    public class SuggestionStatusesConfiguration : IEntityTypeConfiguration<SuggestionStatus>
    {
        public void Configure(EntityTypeBuilder<SuggestionStatus> builder)
        {
            builder.HasData(
              new SuggestionStatus[]
              {
                new SuggestionStatus { Id = 1, Status = "Closed/Not bookable" },
                new SuggestionStatus { Id = 2, Status = "Open/Bookable" },
                new SuggestionStatus { Id = 3, Status = "In progress" },
                new SuggestionStatus { Id = 4, Status = "On moderation" },
              });
        }
    }
}
