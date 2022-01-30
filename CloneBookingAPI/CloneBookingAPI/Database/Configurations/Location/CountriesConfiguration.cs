using CloneBookingAPI.Services.Database.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Location
{
    public class CountriesConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
              new Country[]
              {
                  new Country { Id = 1, Title = "Ukraine" },
                  new Country { Id = 2, Title = "USA" },
                  new Country { Id = 3, Title = "UK" },
                  new Country { Id = 4, Title = "Germany" },
              });
        }
    }
}
