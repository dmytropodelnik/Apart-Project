using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class TripsConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasData(
              new Trip[]
              {

              });
        }
    }
}
