using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Suggestions
{
    public class SurroundingObjectsConfiguration : IEntityTypeConfiguration<SurroundingObject>
    {
        public void Configure(EntityTypeBuilder<SurroundingObject> builder)
        {
            builder.HasData(
              new SurroundingObject[]
              {

              });
        }
    }
}
