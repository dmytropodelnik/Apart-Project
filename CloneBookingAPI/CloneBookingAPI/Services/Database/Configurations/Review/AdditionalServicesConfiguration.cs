using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Review
{
    public class AdditionalServicesConfiguration : IEntityTypeConfiguration<Models.Review.AdditionalService>
    {
        public void Configure(EntityTypeBuilder<Models.Review.AdditionalService> builder)
        {
            builder.HasData(
              new Models.Review.AdditionalService[]
              {
                  new Models.Review.AdditionalService { Id = 1, Category = "Staff" },
                  new Models.Review.AdditionalService { Id = 2, Category = "Facilities" },
                  new Models.Review.AdditionalService { Id = 3, Category = "Cleanliness" },
                  new Models.Review.AdditionalService { Id = 4, Category = "Comfort" },
                  new Models.Review.AdditionalService { Id = 5, Category = "Value for money" },
                  new Models.Review.AdditionalService { Id = 6, Category = "Location" },
                  new Models.Review.AdditionalService { Id = 7, Category = "Free WiFi" },
                  new Models.Review.AdditionalService { Id = 8, Category = "Pets allowed" },
                  new Models.Review.AdditionalService { Id = 9, Category = "Air conditioning" },
                  new Models.Review.AdditionalService { Id = 10, Category = "Private bathroom" },
                  new Models.Review.AdditionalService { Id = 11, Category = "City view" },
                  new Models.Review.AdditionalService { Id = 12, Category = "Private bathroom" },
              });
        }
    }
}
