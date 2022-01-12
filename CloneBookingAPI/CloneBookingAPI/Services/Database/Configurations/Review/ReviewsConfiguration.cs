using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Review
{
    public class ReviewsConfiguration : IEntityTypeConfiguration<Models.Review.Review>
    {
        public void Configure(EntityTypeBuilder<Models.Review.Review> builder)
        {
            builder.HasData(
              new Models.Review.Review[]
              {

              });
        }
    }
}
