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
                  new BookingCategory { Id = 1, Category = "Hotels", },
                  new BookingCategory { Id = 2, Category = "Places of interest", },
                  new BookingCategory { Id = 3, Category = "Homes", },
                  new BookingCategory { Id = 4, Category = "Apartments", },
                  new BookingCategory { Id = 5, Category = "Resorts", },
                  new BookingCategory { Id = 6, Category = "Villas", },
                  new BookingCategory { Id = 7, Category = "Hostels", },
                  new BookingCategory { Id = 8, Category = "B&Bs", },
                  new BookingCategory { Id = 9, Category = "Guest houses", },
                  new BookingCategory { Id = 10, Category = "Unique places to stay", },
                  new BookingCategory { Id = 11, Category = "Vacation Homes", },
                  new BookingCategory { Id = 12, Category = "Serviced Apartments", },
                  new BookingCategory { Id = 13, Category = "Glamping", },
                  new BookingCategory { Id = 14, Category = "Cottages", },
                  new BookingCategory { Id = 15, Category = "Cabins", },
                  new BookingCategory { Id = 16, Category = "Motels", },
                  new BookingCategory { Id = 17, Category = "Ryokans", },
                  new BookingCategory { Id = 18, Category = "Riads", },
                  new BookingCategory { Id = 19, Category = "Resort Villages", },
                  new BookingCategory { Id = 20, Category = "Homestays", },
                  new BookingCategory { Id = 21, Category = "Campgrounds", },
                  new BookingCategory { Id = 22, Category = "Country Houses", },
                  new BookingCategory { Id = 23, Category = "Farm Stays", },
                  new BookingCategory { Id = 24, Category = "Boats", },
                  new BookingCategory { Id = 25, Category = "Luxury Tents" },
                  new BookingCategory { Id = 26, Category = "Self-catering Accommodations", },
                  new BookingCategory { Id = 27, Category = "Tiny houses", },
              });
        }
    }
}
