using CloneBookingAPI.Database.Models.UserData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.UserData
{
    public class CustomerInfosConfiguration : IEntityTypeConfiguration<CustomerInfo>
    {
        public void Configure(EntityTypeBuilder<CustomerInfo> builder)
        {
            builder.HasData(
              new CustomerInfo[]
              {
                
              });
        }
    }
}
