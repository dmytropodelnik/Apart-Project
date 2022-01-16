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
              });
        }
    }
}
