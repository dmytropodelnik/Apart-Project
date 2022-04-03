using CloneBookingAPI.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.Suggestions
{
    public class ApartmentsConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasData(
              new Apartment[]
              {
                  new Apartment { Id = 1, SuggestionId = 1, Description = "Test apartment description 1", RoomsAmount = 2, 
                                  GuestsLimit = 3, PriceInUSD = 25, PriceInUserCurrency = 250, BathroomsAmount = 2,},
                  new Apartment { Id = 2, SuggestionId = 15, Description = "Test apartment description 2", RoomsAmount = 3,
                                  GuestsLimit = 5, PriceInUSD = 65, PriceInUserCurrency = 650, BathroomsAmount = 3,},
                  new Apartment { Id = 3, SuggestionId = 27, Description = "Test apartment description 27", RoomsAmount = 4,
                                  GuestsLimit = 8, PriceInUSD = 125, PriceInUserCurrency = 1250, BathroomsAmount = 4,},
                  new Apartment { Id = 4, SuggestionId = 57, Description = "Test apartment description 57", RoomsAmount = 5,
                                  GuestsLimit = 10, PriceInUSD = 225, PriceInUserCurrency = 2250, BathroomsAmount = 5,},
                  new Apartment { Id = 5, SuggestionId = 101, Description = "Test apartment description 101", RoomsAmount = 6,
                                  GuestsLimit = 12, PriceInUSD = 355, PriceInUserCurrency = 3550, BathroomsAmount = 3,},
              });
        }
    }
}
