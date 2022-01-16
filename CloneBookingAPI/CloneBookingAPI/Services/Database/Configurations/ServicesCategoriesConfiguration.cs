using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class ServicesCategoriesConfiguration : IEntityTypeConfiguration<ServiceCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceCategory> builder)
        {
            builder.HasData(
              new ServiceCategory[]
              {
                  new ServiceCategory { Id = 1, Category = "Stays" },
                  new ServiceCategory { Id = 2, Category = "Flights" },
                  new ServiceCategory { Id = 3, Category = "Car rentals" },
                  new ServiceCategory { Id = 4, Category = "Attractions" },
                  new ServiceCategory { Id = 5, Category = "Airport taxis" },
              });
        }
    }
}
