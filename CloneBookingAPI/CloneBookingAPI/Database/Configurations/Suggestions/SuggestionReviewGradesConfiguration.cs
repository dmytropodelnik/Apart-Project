using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Suggestions
{
    public class SuggestionReviewGradesConfiguration : IEntityTypeConfiguration<SuggestionReviewGrade>
    {
        public void Configure(EntityTypeBuilder<SuggestionReviewGrade> builder)
        {
            builder.HasData(
              new SuggestionReviewGrade[]
              {

              });
        }
    }
}
