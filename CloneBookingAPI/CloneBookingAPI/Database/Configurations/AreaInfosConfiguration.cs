using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class AreaInfosConfiguration : IEntityTypeConfiguration<AreaInfo>
    {
        public void Configure(EntityTypeBuilder<AreaInfo> builder)
        {
            builder.HasData(
              new AreaInfo[]
              {

              });
        }
    }
}
