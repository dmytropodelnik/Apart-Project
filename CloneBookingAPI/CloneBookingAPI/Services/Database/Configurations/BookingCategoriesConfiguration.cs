using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class BookingCategoriesConfiguration : IEntityTypeConfiguration<BookingCategory>
    {
        public void Configure(EntityTypeBuilder<BookingCategory> builder)
        {
            builder.HasData(
              new BookingCategory[]
              {
                  new BookingCategory { Id = 1, Category = "Hotels" },
                  new BookingCategory { Id = 2, Category = "Places of interest" },
                  new BookingCategory { Id = 3, Category = "Homes" },
                  new BookingCategory { Id = 4, Category = "Apartments" },
                  new BookingCategory { Id = 5, Category = "Resorts" },
                  new BookingCategory { Id = 6, Category = "Villas" },
                  new BookingCategory { Id = 7, Category = "Hostels" },
                  new BookingCategory { Id = 8, Category = "B&Bs" },
                  new BookingCategory { Id = 9, Category = "Guest houses" },
                  new BookingCategory { Id = 10, Category = "Unique places to stay" },
              });
        }
    }
}
