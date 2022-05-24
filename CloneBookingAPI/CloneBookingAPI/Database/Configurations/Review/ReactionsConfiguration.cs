using CloneBookingAPI.Database.Models.Review;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.Review
{
    public class ReactionsConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            builder.Property(r => r.IsLiked).HasDefaultValue(false);
            builder.Property(r => r.IsDisliked).HasDefaultValue(false);

            builder.HasData(
              new Reaction[]
              {

              });
        }
    }
}
