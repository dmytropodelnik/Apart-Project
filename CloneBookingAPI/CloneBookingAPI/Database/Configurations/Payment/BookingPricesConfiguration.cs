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
                  new BookingPrice { Id = 1, Discount = 0, TotalPrice = 100, FinalPrice = 100, Difference = 0, CurrencyId = 1, },
                  new BookingPrice { Id = 2, Discount = 0, TotalPrice = 110, FinalPrice = 110, Difference = 0, CurrencyId = 1, },
                  new BookingPrice { Id = 3, Discount = 0, TotalPrice = 120, FinalPrice = 120, Difference = 0, CurrencyId = 1, },
                  new BookingPrice { Id = 4, Discount = 0, TotalPrice = 130, FinalPrice = 130, Difference = 0, CurrencyId = 1, },
                  new BookingPrice { Id = 5, Discount = 0, TotalPrice = 140, FinalPrice = 140, Difference = 0, CurrencyId = 1, },
                  new BookingPrice { Id = 6, Discount = 0, TotalPrice = 150, FinalPrice = 150, Difference = 0, CurrencyId = 1, },
                  new BookingPrice { Id = 7, Discount = 0, TotalPrice = 160, FinalPrice = 160, Difference = 0, CurrencyId = 1, },
                  new BookingPrice { Id = 8, Discount = 0, TotalPrice = 170, FinalPrice = 170, Difference = 0, CurrencyId = 1, },
                  new BookingPrice { Id = 9, Discount = 0, TotalPrice = 180, FinalPrice = 180, Difference = 0, CurrencyId = 1, },
                  new BookingPrice { Id = 10, Discount = 0, TotalPrice = 190, FinalPrice = 190, Difference = 0, CurrencyId = 1, },
                  new BookingPrice { Id = 11, Discount = 0, TotalPrice = 200, FinalPrice = 200, Difference = 0, CurrencyId = 1, },
                  new BookingPrice { Id = 12, Discount = 0, TotalPrice = 210, FinalPrice = 210, Difference = 0, CurrencyId = 1, },
              });
        }
    }
}
