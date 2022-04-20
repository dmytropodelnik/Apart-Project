using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class SuggestionsFileModelsConfiguration : IEntityTypeConfiguration<SuggestionFileModel>
    {
        public void Configure(EntityTypeBuilder<SuggestionFileModel> builder)
        {
            builder.HasData(
              new SuggestionFileModel[]
              {
                  new SuggestionFileModel { Id = 1, ImageId = 24, SuggestionId = 1, },
                  new SuggestionFileModel { Id = 2, ImageId = 25, SuggestionId = 7, },
                  new SuggestionFileModel { Id = 3, ImageId = 26, SuggestionId = 11, },
                  new SuggestionFileModel { Id = 4, ImageId = 27, SuggestionId = 15, },
                  new SuggestionFileModel { Id = 5, ImageId = 28, SuggestionId = 33, },
              });
        }
    }
}
