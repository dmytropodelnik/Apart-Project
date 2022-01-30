using CloneBookingAPI.Services.Database.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Services
{
    public class AirportTaxiBookingsConfiguration : IEntityTypeConfiguration<AirportTaxiBooking>
    {
        public void Configure(EntityTypeBuilder<AirportTaxiBooking> builder)
        {
            builder.HasData(
              new AirportTaxiBooking[]
              {

              });
        }
    }
}
