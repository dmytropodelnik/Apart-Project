using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class StayBookingsApartmentsConfiguration : IEntityTypeConfiguration<StayBookingApartment>
    {
        public void Configure(EntityTypeBuilder<StayBookingApartment> builder)
        {
            builder.HasData(
              new StayBookingApartment[]
              {

              });
        }
    }
}
