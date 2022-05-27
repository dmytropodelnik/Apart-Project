using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class SuggestionsFacilitiesConfiguration : IEntityTypeConfiguration<SuggestionFacility>
    {
        public void Configure(EntityTypeBuilder<SuggestionFacility> builder)
        {
            builder.HasData(
              new SuggestionFacility[]
              {
                  new SuggestionFacility { Id = 1, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 2, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 3, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 4, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 5, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 6, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 7, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 8, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 9, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 10, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 11, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 12, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 13, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 14, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 15, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 16, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 17, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 18, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 19, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 20, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 21, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 22, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 23, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 24, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 25, FacilityId = 1, SuggestionId = 1, },
              });
        }
    }
}
