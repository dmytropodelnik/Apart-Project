using CloneBookingAPI.Services.Database.Models.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Payment
{
    public class BookingPricesConfiguration : IEntityTypeConfiguration<BookingPrice>
    {
        public void Configure(EntityTypeBuilder<BookingPrice> builder)
        {
            builder.HasData(
              new BookingPrice[]
              {

              });
        }
    }
}
