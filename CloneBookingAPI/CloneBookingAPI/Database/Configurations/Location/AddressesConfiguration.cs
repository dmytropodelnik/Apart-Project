using CloneBookingAPI.Services.Database.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Location
{
    public class AddressesConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasData(
              new Address[]
              {
                  new Address { Id = 1, AddressText = "St. Deribasovskaya, 23", CountryId = 1, RegionId = 1, CityId = 1, },
                  new Address { Id = 2, AddressText = "St. Deribasovskaya, 42", CountryId = 1, RegionId = 2, CityId = 1,},
                  new Address { Id = 3, AddressText = "St. Khreshchatyk, 72", CountryId = 1, RegionId = 3, CityId = 2, },
                  new Address { Id = 4, AddressText = "St. Khreshchatyk, 44", CountryId = 1, RegionId = 4, CityId = 3, },
              });
        }
    }
}
