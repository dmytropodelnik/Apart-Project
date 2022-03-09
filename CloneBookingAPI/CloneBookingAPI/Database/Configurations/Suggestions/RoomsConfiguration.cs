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
                  new Room { Id = 1, Sleeps = 2, RoomSize = 25, IsSmokingAllowed = false, IsSuite = true,
                    Description = "Test description for room 1", RoomTypeId = 1, PriceInUserCurrency = 850,
                    PriceInUSD = 54, },
                  new Room { Id = 2, Sleeps = 3, RoomSize = 30, IsSmokingAllowed = true, IsSuite = false,
                    Description = "Test description for room 1", RoomTypeId = 2, PriceInUserCurrency = 4200,
                    PriceInUSD = 85, },
                  new Room { Id = 3, Sleeps = 5, RoomSize = 35, IsSmokingAllowed = false, IsSuite = true,
                    Description = "Test description for room 1", RoomTypeId = 3, PriceInUserCurrency = 305,
                    PriceInUSD = 15, },
                  new Room { Id = 4, Sleeps = 4, RoomSize = 45, IsSmokingAllowed = true, IsSuite = false,
                    Description = "Test description for room 1", RoomTypeId = 4, PriceInUserCurrency = 1220,
                    PriceInUSD = 74, },
                  new Room { Id = 5, Sleeps = 1, RoomSize = 15, IsSmokingAllowed = false, IsSuite = false,
                    Description = "Test description for room 1", RoomTypeId = 5, PriceInUserCurrency = 3890,
                    PriceInUSD = 80, },
              });
        }
    }
}
