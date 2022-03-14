using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Suggestions
{
    public class SuggestionsConfiguration : IEntityTypeConfiguration<Suggestion>
    {
        public void Configure(EntityTypeBuilder<Suggestion> builder)
        {
            builder.Property(s => s.Progress).HasDefaultValue(0);
            builder.Property(s => s.UniqueCode).HasDefaultValue(string.Empty);

            builder.HasData(
              new Suggestion[]
              {
                  new Suggestion { Id = 1, Name = "Test Suggestion 1", GuestsAmount = 2, BathroomsAmount = 1,
                    RoomsAmount = 3, StarRating = 3, Progress = 100, Description = "Test description of test suggestion 1",
                    PriceInUserCurrency = 10121, PriceInUSD = 51, IsParkingAvailable = true, UserId = 1, ServiceCategoryId = 1,
                    BookingCategoryId = 1, AddressId = 1,
                  },
                  new Suggestion { Id = 2, Name = "Test Suggestion 2", GuestsAmount = 1, BathroomsAmount = 3,
                    RoomsAmount = 6, StarRating = 4, Progress = 100, Description = "Test description of test suggestion 2",
                    PriceInUserCurrency = 1010, PriceInUSD = 10, IsParkingAvailable = false, UserId = 2, ServiceCategoryId = 1,
                    BookingCategoryId = 2, AddressId = 2,
                  },
                  new Suggestion { Id = 3, Name = "Test Suggestion 3", GuestsAmount = 5, BathroomsAmount = 2,
                    RoomsAmount = 5, StarRating = 5, Progress = 100, Description = "Test description of test suggestion 3",
                    PriceInUserCurrency = 2032, PriceInUSD = 21, IsParkingAvailable = true, UserId = 3, ServiceCategoryId = 1,
                    BookingCategoryId = 3, AddressId = 3, 
                  },
                  new Suggestion { Id = 4, Name = "Test Suggestion 4", GuestsAmount = 4, BathroomsAmount = 2,
                    RoomsAmount = 4, StarRating = 5, Progress = 100, Description = "Test description of test suggestion 4",
                    PriceInUserCurrency = 2100, PriceInUSD = 31, IsParkingAvailable = true, UserId = 1, ServiceCategoryId = 1,
                    BookingCategoryId = 4, AddressId = 4,
                  },
                  new Suggestion { Id = 5, Name = "Test Suggestion 5", GuestsAmount = 3, BathroomsAmount = 3,
                    RoomsAmount = 6, StarRating = 5, Progress = 100, Description = "Test description of test suggestion 5",
                    PriceInUserCurrency = 1905, PriceInUSD = 24, IsParkingAvailable = false, UserId = 1, ServiceCategoryId = 1,
                    BookingCategoryId = 5, AddressId = 1,
                  }
              });
        }
    }
}
