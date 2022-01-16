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
                  new BookingCategory { Id = 1, Category = "Places of interest" },
                  new BookingCategory { Id = 1, Category = "Homes" },
                  new BookingCategory { Id = 1, Category = "Apartments" },
                  new BookingCategory { Id = 1, Category = "Resorts" },
                  new BookingCategory { Id = 1, Category = "Villas" },
                  new BookingCategory { Id = 1, Category = "Hostels" },
                  new BookingCategory { Id = 1, Category = "B&Bs" },
                  new BookingCategory { Id = 1, Category = "Guest houses" },
                  new BookingCategory { Id = 1, Category = "Unique places to stay" },
              });
        }
    }
}
