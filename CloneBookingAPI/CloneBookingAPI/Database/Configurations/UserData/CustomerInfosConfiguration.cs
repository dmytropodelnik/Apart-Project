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
                new CustomerInfo { Id = 1, AddressText = "St. Deribasovskaya, 23", City = "Odesa", Country = "Ukraine", Email = "apartproject@ukr.net", FirstName = "Dmytro", LastName = "Podelnik", PhoneNumber = "380505852173", ZipCode = "65088", },
                new CustomerInfo { Id = 2, AddressText = "St. Deribasovskaya, 42", City = "Odesa", Country = "Ukraine", Email = "inko10092001@gmail.com", FirstName = "Igor", LastName = "Podelnik", PhoneNumber = "380508753678", ZipCode = "65101", },
                new CustomerInfo { Id = 3, AddressText = "St. Khreshchatyk, 72", City = "Kyiv", Country = "Ukraine", Email = "dmitrypodelnik.developer@gmail.com", FirstName = "Tatyana", LastName = "Podelnik", PhoneNumber = "380684327932", ZipCode = "74632", },
              });
        }
    }
}
