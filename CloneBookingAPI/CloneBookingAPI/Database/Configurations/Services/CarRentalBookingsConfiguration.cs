using CloneBookingAPI.Services.Database.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Services
{
    public class CarRentalBookingsConfiguration : IEntityTypeConfiguration<CarRentalBooking>
    {
        public void Configure(EntityTypeBuilder<CarRentalBooking> builder)
        {
            builder.HasData(
              new CarRentalBooking[]
              {

              });
        }
    }
}
