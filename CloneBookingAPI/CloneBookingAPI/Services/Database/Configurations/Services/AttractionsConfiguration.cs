using CloneBookingAPI.Services.Database.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Services
{
    public class AttractionsConfiguration : IEntityTypeConfiguration<Attraction>
    {
        public void Configure(EntityTypeBuilder<Attraction> builder)
        {
            builder.HasData(
              new Attraction[]
              {

              });
        }
    }
}
