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
                  new City { Id = 1, Title = "Odesa" },
                  new City { Id = 2, Title = "Kyiv" },
                  new City { Id = 3, Title = "New York" },
                  new City { Id = 4, Title = "Berlin" },
              });
        }
    }
}
