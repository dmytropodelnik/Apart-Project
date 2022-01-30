using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class AreaInfoTypesConfiguration : IEntityTypeConfiguration<AreaInfoType>
    {
        public void Configure(EntityTypeBuilder<AreaInfoType> builder)
        {
            builder.HasData(
              new AreaInfoType[]
              {

              });
        }
    }
}
