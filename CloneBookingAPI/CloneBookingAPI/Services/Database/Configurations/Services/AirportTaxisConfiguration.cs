using CloneBookingAPI.Services.Database.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Services
{
    public class AirportTaxisConfiguration : IEntityTypeConfiguration<AirportTaxi>
    {
        public void Configure(EntityTypeBuilder<AirportTaxi> builder)
        {
            builder.HasData(
              new AirportTaxi[]
              {

              });
        }
    }
}
