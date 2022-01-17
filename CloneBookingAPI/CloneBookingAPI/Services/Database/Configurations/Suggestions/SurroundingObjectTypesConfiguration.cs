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

              });
        }
    }
}
