using CloneBookingAPI.Services.Database.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Location
{
    public class DistrictsConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasData(
              new District[]
              {

              });
        }
    }
}
