using CloneBookingAPI.Services.Database.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Location
{
    public class AirportsConfiguration : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.HasData(
              new Airport[]
              {

              });
        }
    }
}
