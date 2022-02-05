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
                  new Models.UserProfile.Favorite { Id = 1, UserId = 1 },
                  new Models.UserProfile.Favorite { Id = 2, UserId = 2 },
                  new Models.UserProfile.Favorite { Id = 3, UserId = 3 },
                  new Models.UserProfile.Favorite { Id = 4, UserId = 4 },
              });
        }
    }
}
