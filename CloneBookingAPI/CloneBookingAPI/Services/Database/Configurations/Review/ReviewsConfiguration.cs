using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Review
{
    public class ReviewsConfiguration : IEntityTypeConfiguration<Models.Review.Review>
    {
        public void Configure(EntityTypeBuilder<Models.Review.Review> builder)
        {
            builder.Property(r => r.LikesAmount).HasDefaultValue(0);
            builder.Property(r => r.DislikesAmount).HasDefaultValue(0);

            builder.HasData(
              new Models.Review.Review[]
              {

              });
        }
    }
}
