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
                  new Region { Id = 1, Title = "Test1", ImageId = 2, },
                  new Region { Id = 2, Title = "Test2", ImageId = 2, },
                  new Region { Id = 3, Title = "Test3", ImageId = 2, },
                  new Region { Id = 4, Title = "Test4", ImageId = 2, },
                  new Region { Id = 5, Title = "Test5", ImageId = 2, },
                  new Region { Id = 6, Title = "Test6", ImageId = 2, },
                  new Region { Id = 7, Title = "Test7", ImageId = 2, },
                  new Region { Id = 8, Title = "Test8", ImageId = 2, },
                  new Region { Id = 9, Title = "Test9", ImageId = 2, },
                  new Region { Id = 10, Title = "Test10", ImageId = 2, },
                  new Region { Id = 11, Title = "Test11", ImageId = 2, },
                  new Region { Id = 12, Title = "Test12", ImageId = 2, },
                  new Region { Id = 13, Title = "Test13", ImageId = 1, },
                  new Region { Id = 14, Title = "Test14", ImageId = 1, },
              });
        }
    }
}
