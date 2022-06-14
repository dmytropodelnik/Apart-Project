using CloneBookingAPI.Database.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.Services
{
    public class BookingStatusesConfiguration : IEntityTypeConfiguration<BookingStatus>
    {
        public void Configure(EntityTypeBuilder<BookingStatus> builder)
        {
            builder.HasData(
              new BookingStatus[]
              {
                new BookingStatus { Id = 1, Status = "Reserved" },
                new BookingStatus { Id = 2, Status = "Booked" },
                new BookingStatus { Id = 3, Status = "Paid" },
                new BookingStatus { Id = 4, Status = "Cancelled" },
              });
        }
    }
}
