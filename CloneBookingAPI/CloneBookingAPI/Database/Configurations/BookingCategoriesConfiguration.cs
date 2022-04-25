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
                  new BookingCategory { Id = 1, Category = "Hotels", ImageId = 17, BookingCategoryTypeId = 3, },
                  new BookingCategory { Id = 2, Category = "Apartments", ImageId = 18, BookingCategoryTypeId = 1, },
                  new BookingCategory { Id = 3, Category = "Resorts", ImageId = 19, BookingCategoryTypeId = 4, },
                  new BookingCategory { Id = 4, Category = "Villas", ImageId = 20, BookingCategoryTypeId = 2, },
                  new BookingCategory { Id = 5, Category = "Hostels", ImageId = 21, BookingCategoryTypeId = 3, },
                  new BookingCategory { Id = 6, Category = "B&Bs", ImageId = 22, BookingCategoryTypeId = 3, },
                  new BookingCategory { Id = 7, Category = "Guest Houses", ImageId = 23, BookingCategoryTypeId = 2, },
                  new BookingCategory { Id = 8, Category = "Vacation Homes", ImageId = 24, BookingCategoryTypeId = 2, },
                  new BookingCategory { Id = 9, Category = "Serviced Apartments", ImageId = 25, BookingCategoryTypeId = 1, },
                  new BookingCategory { Id = 10, Category = "Glamping", ImageId = 26, BookingCategoryTypeId = 4, },
                  new BookingCategory { Id = 11, Category = "Cottages", ImageId = 27, BookingCategoryTypeId = 2, },
                  new BookingCategory { Id = 12, Category = "Cabins", ImageId = 28, BookingCategoryTypeId = 4, },
                  new BookingCategory { Id = 13, Category = "Motels", ImageId = 29, BookingCategoryTypeId = 3, },
                  new BookingCategory { Id = 14, Category = "Ryokans", ImageId = 30, BookingCategoryTypeId = 4, },
                  new BookingCategory { Id = 15, Category = "Riads", ImageId = 31, BookingCategoryTypeId = 4, },
                  new BookingCategory { Id = 16, Category = "Resort Villages", ImageId = 32, BookingCategoryTypeId = 4, },
                  new BookingCategory { Id = 17, Category = "Homestays", ImageId = 33, BookingCategoryTypeId = 2, },
                  new BookingCategory { Id = 18, Category = "Campgrounds", ImageId = 34, BookingCategoryTypeId = 4, },
                  new BookingCategory { Id = 19, Category = "Country Houses", ImageId = 35, BookingCategoryTypeId = 2, },
                  new BookingCategory { Id = 20, Category = "Farm Stays", ImageId = 36, BookingCategoryTypeId = 4, },
                  new BookingCategory { Id = 21, Category = "Boats", ImageId = 37, BookingCategoryTypeId = 4, },
                  new BookingCategory { Id = 22, Category = "Luxury Tents", ImageId = 38, BookingCategoryTypeId = 4, },
                  new BookingCategory { Id = 23, Category = "Selfcatering Accommodation", ImageId = 39, BookingCategoryTypeId = 4, },
                  new BookingCategory { Id = 24, Category = "Tiny Houses", ImageId = 40, BookingCategoryTypeId = 2, },
              });
        }
    }
}
