using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class NotificationsConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasData(
              new Notification[]
              {

              });
        }
    }
}
