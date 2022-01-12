using CloneBookingAPI.Services.Database.Models;
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

              });
        }
    }
}
