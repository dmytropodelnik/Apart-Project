using CloneBookingAPI.Services.Database.Models.UserData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.UserData
{
    public class TempUsersConfiguration : IEntityTypeConfiguration<TempUser>
    {
        public void Configure(EntityTypeBuilder<TempUser> builder)
        {
            builder.HasData(
              new TempUser[]
              {

              });
        }
    }
}
