using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Review
{
    public class ReviewMessagesConfiguration : IEntityTypeConfiguration<ReviewMessage>
    {
        public void Configure(EntityTypeBuilder<ReviewMessage> builder)
        {
            builder.HasData(
              new ReviewMessage[]
              {

              });
        }
    }
}
