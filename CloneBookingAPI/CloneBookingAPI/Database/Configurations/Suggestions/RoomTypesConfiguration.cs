using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Suggestions
{
    public class RoomTypesConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasData(
              new RoomType[]
              {
                  new RoomType { Id = 1, Title = "Bedroom", },
                  new RoomType { Id = 2, Title = "Living room", },
                  new RoomType { Id = 3, Title = "Bathroom", },
              });
        }
    }
}
