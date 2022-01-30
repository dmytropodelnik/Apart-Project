using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Review
{
    public class ReviewCategoriesConfiguration : IEntityTypeConfiguration<Models.Review.ReviewCategory>
    {
        public void Configure(EntityTypeBuilder<Models.Review.ReviewCategory> builder)
        {
            builder.HasData(
              new Models.Review.ReviewCategory[]
              {
                  new Models.Review.ReviewCategory { Id = 1, Category = "Staff" },
                  new Models.Review.ReviewCategory { Id = 2, Category = "Facilities" },
                  new Models.Review.ReviewCategory { Id = 3, Category = "Cleanliness" },
                  new Models.Review.ReviewCategory { Id = 4, Category = "Comfort" },
                  new Models.Review.ReviewCategory { Id = 5, Category = "Value for money" },
                  new Models.Review.ReviewCategory { Id = 6, Category = "Location" },
                  new Models.Review.ReviewCategory { Id = 7, Category = "Free WiFi" },
                  new Models.Review.ReviewCategory { Id = 8, Category = "Pets allowed" },
                  new Models.Review.ReviewCategory { Id = 9, Category = "Air conditioning" },
                  new Models.Review.ReviewCategory { Id = 10, Category = "Private bathroom" },
                  new Models.Review.ReviewCategory { Id = 11, Category = "City view" },
                  new Models.Review.ReviewCategory { Id = 12, Category = "Private bathroom" },
              });
        }
    }
}
