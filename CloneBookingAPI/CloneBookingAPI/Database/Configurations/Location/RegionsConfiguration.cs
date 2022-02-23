using CloneBookingAPI.Services.Database.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Location
{
    public class RegionsConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasData(
              new Region[]
              {
                  new Region { Id = 1, Title = "Test", ImageId = 2, },
                  new Region { Id = 2, Title = "Test", ImageId = 2, },
                  new Region { Id = 3, Title = "Test", ImageId = 2, },
                  new Region { Id = 4, Title = "Test", ImageId = 2, },
                  new Region { Id = 5, Title = "Test", ImageId = 2, },
                  new Region { Id = 6, Title = "Test", ImageId = 2, },
                  new Region { Id = 7, Title = "Test", ImageId = 2, },
                  new Region { Id = 8, Title = "Test", ImageId = 2, },
                  new Region { Id = 9, Title = "Test", ImageId = 2, },
                  new Region { Id = 10, Title = "Test", ImageId = 2, },
                  new Region { Id = 11, Title = "Test", ImageId = 2, },
                  new Region { Id = 12, Title = "Test", ImageId = 2, },
                  new Region { Id = 13, Title = "Test", ImageId = 1, },
                  new Region { Id = 14, Title = "Test", ImageId = 1, },
              });
        }
    }
}
