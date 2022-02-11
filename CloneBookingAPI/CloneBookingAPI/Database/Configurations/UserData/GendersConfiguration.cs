using CloneBookingAPI.Services.Database.Models.UserProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.UserProfile
{
    public class GendersConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasData(
              new Gender[]
              {
                  new Gender { Id = 1, Title = "Male" },
                  new Gender { Id = 2, Title = "Female" },
                  new Gender { Id = 3, Title = "test" },
              });
        }
    }
}
