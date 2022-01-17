using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class BookingsConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(b => b.IsForWork).HasDefaultValue(false);
            builder.Property(b => b.IsRequestedAirportShuttle).HasDefaultValue(false);
            builder.Property(b => b.IsRequestedRentingCar).HasDefaultValue(false);

            builder.HasData(
              new Booking[]
              {

              });
        }
    }
}
