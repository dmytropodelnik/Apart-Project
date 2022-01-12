using CloneBookingAPI.Services.Database.Models.Flights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Flights
{
    public class FlightClassTypesConfiguration : IEntityTypeConfiguration<FlightClassType>
    {
        public void Configure(EntityTypeBuilder<FlightClassType> builder)
        {
            builder.HasData(
              new FlightClassType[]
              {

              });
        }
    }
}
