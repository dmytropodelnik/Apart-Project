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
                  new Region { Id = 1, Title = "Zanzibar", ImageId = 2, CityId = 1, },
                  new Region { Id = 2, Title = "Ibiza", ImageId = 2, CityId = 2, },
                  new Region { Id = 3, Title = "Bihar", ImageId = 2, CityId = 3,},
                  new Region { Id = 4, Title = "Bali", ImageId = 2, CityId = 4,},
                  new Region { Id = 5, Title = "Ras Al Khaimah", ImageId = 2, CityId = 1,},
                  new Region { Id = 6, Title = "Uttar Pradesh", ImageId = 2, CityId = 2,},
                  new Region { Id = 7, Title = "Texel", ImageId = 2, CityId = 3,},
                  new Region { Id = 8, Title = "Isle of Wight", ImageId = 2, CityId = 4,},
                  new Region { Id = 9, Title = "Jersey", ImageId = 2, CityId = 1,},
                  new Region { Id = 10, Title = "Mykonos", ImageId = 2, CityId = 2,},
                  new Region { Id = 11, Title = "Santorini", ImageId = 2, CityId = 3,},
                  new Region { Id = 12, Title = "Cornwall", ImageId = 2, CityId = 4,},
                  new Region { Id = 13, Title = "England", ImageId = 2, CityId = 1,},
                  new Region { Id = 14, Title = "Tenerife", ImageId = 2, CityId = 2,},
              });
        }
    }
}
