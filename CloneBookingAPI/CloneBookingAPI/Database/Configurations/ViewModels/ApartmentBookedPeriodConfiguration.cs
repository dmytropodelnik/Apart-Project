using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class ApartmentBookedPeriodConfiguration : IEntityTypeConfiguration<ApartmentBookedPeriod>
    {
        public void Configure(EntityTypeBuilder<ApartmentBookedPeriod> builder)
        {
            builder.HasData(
              new ApartmentBookedPeriod[]
              {

              });
        }
    }
}
