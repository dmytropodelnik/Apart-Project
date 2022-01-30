using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Suggestions
{
    public class RoomsConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(r => r.IsSuite).HasDefaultValue(false);

            builder.HasData(
              new Room[]
              {

              });
        }
    }
}
