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
                  new BookingCategory { Id = 1, Category = "Hotels", ImageId = 1, },
                  new BookingCategory { Id = 2, Category = "Places Of Interest", ImageId = 1, },
                  new BookingCategory { Id = 3, Category = "Homes", ImageId = 1, },
                  new BookingCategory { Id = 4, Category = "Apartments", ImageId = 1, },
                  new BookingCategory { Id = 5, Category = "Resorts", ImageId = 1, },
                  new BookingCategory { Id = 6, Category = "Villas", ImageId = 1, },
                  new BookingCategory { Id = 7, Category = "Hostels", ImageId = 1, },
                  new BookingCategory { Id = 8, Category = "B&Bs", ImageId = 1, },
                  new BookingCategory { Id = 9, Category = "Guest Houses", ImageId = 1, },
                  new BookingCategory { Id = 10, Category = "Unique Places To Stay", ImageId = 1, },
                  new BookingCategory { Id = 11, Category = "Vacation Homes", ImageId = 1, },
                  new BookingCategory { Id = 12, Category = "Serviced Apartments", ImageId = 1, },
                  new BookingCategory { Id = 13, Category = "Glamping", ImageId = 1, },
                  new BookingCategory { Id = 14, Category = "Cottages", ImageId = 1, },
                  new BookingCategory { Id = 15, Category = "Cabins", ImageId = 1, },
                  new BookingCategory { Id = 16, Category = "Motels", ImageId = 1, },
                  new BookingCategory { Id = 17, Category = "Ryokans", ImageId = 1, },
                  new BookingCategory { Id = 18, Category = "Riads", ImageId = 1, },
                  new BookingCategory { Id = 19, Category = "Resort Villages", ImageId = 1, },
                  new BookingCategory { Id = 20, Category = "Homestays", ImageId = 1, },
                  new BookingCategory { Id = 21, Category = "Campgrounds", ImageId = 1, },
                  new BookingCategory { Id = 22, Category = "Country Houses", ImageId = 1, },
                  new BookingCategory { Id = 23, Category = "Farm Stays", ImageId = 1, },
                  new BookingCategory { Id = 24, Category = "Boats", ImageId = 1, },
                  new BookingCategory { Id = 25, Category = "Luxury Tents", ImageId = 1, },
                  new BookingCategory { Id = 26, Category = "Selfcatering Accommodation", ImageId = 1, },
                  new BookingCategory { Id = 27, Category = "Tiny Houses", ImageId = 1, },
              });
        }
    }
}
