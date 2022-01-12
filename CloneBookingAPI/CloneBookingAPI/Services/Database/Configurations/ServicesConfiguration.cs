using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class ServicesConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasData(
              new Service[]
              {

              });
        }
    }
}
