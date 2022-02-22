using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.UserProfile
{
    public class RolesConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
              new Role[]
              {
                    new Role { Id = 1, Name = "admin" },
                    new Role { Id = 2, Name = "user" },
                    new Role { Id = 3, Name = "temp user" },
              });
        }
    }
}
