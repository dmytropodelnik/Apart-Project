using CloneBookingAPI.Services.Database.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Services
{
    public class AttractionBookingsConfiguration : IEntityTypeConfiguration<AttractionBooking>
    {
        public void Configure(EntityTypeBuilder<AttractionBooking> builder)
        {
            builder.HasData(
              new AttractionBooking[]
              {

              });
        }
    }
}
