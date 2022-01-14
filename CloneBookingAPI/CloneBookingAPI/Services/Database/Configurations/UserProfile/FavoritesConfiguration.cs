using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.UserProfile
{
    public class FavoritesConfiguration : IEntityTypeConfiguration<Models.UserProfile.Favorite>
    {
        public void Configure(EntityTypeBuilder<Models.UserProfile.Favorite> builder)
        {
            builder.HasData(
              new Models.UserProfile.Favorite[]
              {

              });
        }
    }
}
