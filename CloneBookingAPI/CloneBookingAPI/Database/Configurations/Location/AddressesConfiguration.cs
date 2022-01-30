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
                  new Address { Id = 1, AddressText = "St. Deribasovskaya, 23", },
                  new Address { Id = 2, AddressText = "St. Mark’s Place 23", },
                  new Address { Id = 3, AddressText = "St. Legiendamm 4", },
                  new Address { Id = 4, AddressText = "St. Khreshchatyk, 44", },
              });
        }
    }
}
