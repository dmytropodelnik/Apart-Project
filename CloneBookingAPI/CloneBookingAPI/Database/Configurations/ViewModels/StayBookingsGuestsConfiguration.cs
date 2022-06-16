using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class StayBookingsGuestsConfiguration : IEntityTypeConfiguration<StayBookingGuest>
    {
        public void Configure(EntityTypeBuilder<StayBookingGuest> builder)
        {
            builder.HasData(
              new StayBookingGuest[]
              {
                new StayBookingGuest { Id = 1, GuestId = 1, StayBookingId = 1, },
                new StayBookingGuest { Id = 2, GuestId = 2, StayBookingId = 1, },
                new StayBookingGuest { Id = 3, GuestId = 3, StayBookingId = 2, },
                new StayBookingGuest { Id = 4, GuestId = 4, StayBookingId = 2, },
                new StayBookingGuest { Id = 5, GuestId = 5, StayBookingId = 2, },
                new StayBookingGuest { Id = 6, GuestId = 6, StayBookingId = 3, },
                new StayBookingGuest { Id = 7, GuestId = 7, StayBookingId = 3, },
                new StayBookingGuest { Id = 8, GuestId = 8, StayBookingId = 3, },
                new StayBookingGuest { Id = 9, GuestId = 9, StayBookingId = 4, },
                new StayBookingGuest { Id = 10, GuestId = 10, StayBookingId = 5, },
                new StayBookingGuest { Id = 11, GuestId = 11, StayBookingId = 5, },
                new StayBookingGuest { Id = 12, GuestId = 12, StayBookingId = 6, },
                new StayBookingGuest { Id = 13, GuestId = 13, StayBookingId = 6, },
                new StayBookingGuest { Id = 14, GuestId = 14, StayBookingId = 7, },
                new StayBookingGuest { Id = 15, GuestId = 15, StayBookingId = 7, },
                new StayBookingGuest { Id = 16, GuestId = 16, StayBookingId = 7, },
                new StayBookingGuest { Id = 17, GuestId = 17, StayBookingId = 8, },
                new StayBookingGuest { Id = 18, GuestId = 18, StayBookingId = 8, },
                new StayBookingGuest { Id = 19, GuestId = 19, StayBookingId = 9, },
              });
        }
    }
}
