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

              });
        }
    }
}
