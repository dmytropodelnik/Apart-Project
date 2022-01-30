using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class FacilityTypesConfiguration : IEntityTypeConfiguration<FacilityType>
    {
        public void Configure(EntityTypeBuilder<FacilityType> builder)
        {
            builder.HasData(
              new FacilityType[]
              {

              });
        }
    }
}
