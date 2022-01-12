using CloneBookingAPI.Services.Database.Models;
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
                  
              });
        }
    }
}
