using CloneBookingAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations
{
    public class BookingCategoryTypesConfiguration : IEntityTypeConfiguration<BookingCategoryType>
    {
        public void Configure(EntityTypeBuilder<BookingCategoryType> builder)
        {
            builder.HasData(
              new BookingCategoryType[]
              {
                  new BookingCategoryType { Id = 1, Type = "Apartment" },
                  new BookingCategoryType { Id = 2, Type = "Home" },
                  new BookingCategoryType { Id = 3, Type = "Hotel" },
                  new BookingCategoryType { Id = 4, Type = "Alternative place" },
              });
        }
    }
}
