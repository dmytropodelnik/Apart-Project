using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class StayBookingsConfiguration : IEntityTypeConfiguration<StayBooking>
    {
        public void Configure(EntityTypeBuilder<StayBooking> builder)
        {
            builder.Property(b => b.IsForWork).HasDefaultValue(false);
            builder.Property(b => b.IsRevealed).HasDefaultValue(true);
            builder.Property(b => b.IsPaid).HasDefaultValue(false);

            builder.HasData(
              new StayBooking[]
              {
                  new StayBooking { Id = 1, BookingStatusId = 1, CheckIn = DateTime.UtcNow, CheckOut = DateTime.Now, CustomerInfoId = 1, IsForWork = false, IsPaid = false, IsRevealed = true, Nights = 1, PIN = "000001", UniqueNumber = "0000001", PriceId = 1, SpecialRequests = "Special requests for stay booking 1.", SuggestionId = 101, UserId = 1,  },
                  new StayBooking { Id = 2, BookingStatusId = 2, CheckIn = DateTime.UtcNow, CheckOut = DateTime.Now, CustomerInfoId = 1, IsForWork = false, IsPaid = false, IsRevealed = true, Nights = 1, PIN = "000002", UniqueNumber = "0000002", PriceId = 2, SpecialRequests = "Special requests for stay booking 2.", SuggestionId = 1, UserId = 1,  },
                  new StayBooking { Id = 3, BookingStatusId = 2, CheckIn = DateTime.UtcNow, CheckOut = DateTime.Now, CustomerInfoId = 2, IsForWork = true, IsPaid = false, IsRevealed = true, Nights = 1, PIN = "000003", UniqueNumber = "0000003", PriceId = 3, SpecialRequests = "Special requests for stay booking 3.", SuggestionId = 13, UserId = 2,  },
                  new StayBooking { Id = 4, BookingStatusId = 1, CheckIn = DateTime.UtcNow, CheckOut = DateTime.Now, CustomerInfoId = 2, IsForWork = true, IsPaid = false, IsRevealed = true, Nights = 1, PIN = "000004", UniqueNumber = "0000004", PriceId = 4, SpecialRequests = "Special requests for stay booking 4.", SuggestionId = 1, UserId = 2,  },
                  new StayBooking { Id = 5, BookingStatusId = 3, CheckIn = DateTime.UtcNow, CheckOut = DateTime.Now, CustomerInfoId = 3, IsForWork = false, IsPaid = true, IsRevealed = true, Nights = 1, PIN = "000005", UniqueNumber = "0000005", PriceId = 5, SpecialRequests = "Special requests for stay booking 5.", SuggestionId = 13, UserId = 3,  },
                  new StayBooking { Id = 6, BookingStatusId = 3, CheckIn = DateTime.UtcNow, CheckOut = DateTime.Now, CustomerInfoId = 3, IsForWork = true, IsPaid = true, IsRevealed = true, Nights = 1, PIN = "000006", UniqueNumber = "0000006", PriceId = 6, SpecialRequests = "Special requests for stay booking 6.", SuggestionId = 15, UserId = 3,  },
                  new StayBooking { Id = 7, BookingStatusId = 3, CheckIn = DateTime.UtcNow, CheckOut = DateTime.Now, CustomerInfoId = 1, IsForWork = false, IsPaid = true, IsRevealed = true, Nights = 1, PIN = "000007", UniqueNumber = "0000007", PriceId = 7, SpecialRequests = "Special requests for stay booking 7.", SuggestionId = 1, UserId = 1,  },
                  new StayBooking { Id = 8, BookingStatusId = 2, CheckIn = DateTime.UtcNow, CheckOut = DateTime.Now, CustomerInfoId = 2, IsForWork = false, IsPaid = false, IsRevealed = true, Nights = 1, PIN = "000008", UniqueNumber = "0000008", PriceId = 8, SpecialRequests = "Special requests for stay booking 8.", SuggestionId = 15, UserId = 2,  },
                  new StayBooking { Id = 9, BookingStatusId = 1, CheckIn = DateTime.UtcNow, CheckOut = DateTime.Now, CustomerInfoId = 1, IsForWork = true, IsPaid = false, IsRevealed = true, Nights = 1, PIN = "000009", UniqueNumber = "0000009", PriceId = 9, SpecialRequests = "Special requests for stay booking 9.", SuggestionId = 101, UserId = 1,  },

              });
        }
    }
}
