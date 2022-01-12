using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class FacilitiesConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.HasData(
              new Facility[]
              {

              });
        }
    }
}
