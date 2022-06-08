using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class StayBookingsConfiguration : IEntityTypeConfiguration<StayBooking>
    {
        public void Configure(EntityTypeBuilder<StayBooking> builder)
        {
            builder.Property(b => b.IsForWork).HasDefaultValue(false);

            builder.HasData(
              new StayBooking[]
              {

              });
        }
    }
}
