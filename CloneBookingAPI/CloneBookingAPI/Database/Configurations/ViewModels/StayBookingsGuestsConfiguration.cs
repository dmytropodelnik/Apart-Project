using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class StayBookingsGuestsConfiguration : IEntityTypeConfiguration<StayBookingsGuests>
    {
        public void Configure(EntityTypeBuilder<StayBookingsGuests> builder)
        {
            builder.HasData(
              new StayBookingsGuests[]
              {

              });
        }
    }
}
