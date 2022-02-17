using CloneBookingAPI.Services.Database.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Location
{
    public class CitiesConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(
              new City[]
              {
                  new City { Id = 1, Title = "Odesa", ImageId = 2, CountryId = 1, },
                  new City { Id = 2, Title = "Kyiv", ImageId = 2, CountryId = 1, },
                  new City { Id = 3, Title = "Kyiv", ImageId = 2, CountryId = 1, },
                  new City { Id = 4, Title = "Kyiv", ImageId = 2, CountryId = 1, },
                  new City { Id = 5, Title = "Kyiv", ImageId = 2, CountryId = 1, },
                  new City { Id = 6, Title = "Kyiv", ImageId = 2, CountryId = 1, },
                  new City { Id = 7, Title = "Kyiv", ImageId = 2, CountryId = 1, },
                  new City { Id = 8, Title = "Kyiv", ImageId = 2, CountryId = 1, },
                  new City { Id = 9, Title = "Kyiv", ImageId = 2, CountryId = 1, },
                  new City { Id = 10, Title = "Kyiv", ImageId = 2, CountryId = 1, },
                  new City { Id = 11, Title = "Kyiv", ImageId = 2, CountryId = 1, },
                  new City { Id = 12, Title = "Kyiv", ImageId = 2, CountryId = 1, },
                  new City { Id = 13, Title = "New York", ImageId = 1, CountryId = 2, },
                  new City { Id = 14, Title = "Berlin", ImageId = 1, CountryId = 114, },
              });
        }
    }
}
