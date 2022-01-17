using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.UserProfile
{
    public class UserProfilesConfiguration : IEntityTypeConfiguration<Models.UserProfile.UserProfile>
    {
        public void Configure(EntityTypeBuilder<Models.UserProfile.UserProfile> builder)
        {
            builder.HasData(
              new Models.UserProfile.UserProfile[]
              {

              });
        }
    }
}
