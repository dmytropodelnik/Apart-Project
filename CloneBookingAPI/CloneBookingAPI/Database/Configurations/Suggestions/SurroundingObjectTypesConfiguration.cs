using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Suggestions
{
    public class SurroundingObjectTypesConfiguration : IEntityTypeConfiguration<SurroundingObjectType>
    {
        public void Configure(EntityTypeBuilder<SurroundingObjectType> builder)
        {
            builder.HasData(
              new SurroundingObjectType[]
              {
                  new SurroundingObjectType { Id = 1, Type = "What's nearby", },
                  new SurroundingObjectType { Id = 2, Type = "Beaches in the Neighborhood", },
                  new SurroundingObjectType { Id = 3, Type = "Closest Airports", },
                  new SurroundingObjectType { Id = 4, Type = "Top attractions", },
                  new SurroundingObjectType { Id = 5, Type = "Public transit", },
                  new SurroundingObjectType { Id = 6, Type = "Restaurants & cafes", },
                  new SurroundingObjectType { Id = 7, Type = "Natural Beauty", },
              });
        }
    }
}
