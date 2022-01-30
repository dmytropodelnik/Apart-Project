using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class FlightBookingsConfiguration : IEntityTypeConfiguration<FlightBooking>
    {
        public void Configure(EntityTypeBuilder<FlightBooking> builder)
        {
            builder.HasData(
              new FlightBooking[]
              {

              });
        }
    }
}
