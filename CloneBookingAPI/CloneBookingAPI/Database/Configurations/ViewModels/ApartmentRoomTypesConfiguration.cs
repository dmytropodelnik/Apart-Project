using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class ApartmentRoomTypesConfiguration : IEntityTypeConfiguration<ApartmentRoomType>
    {
        public void Configure(EntityTypeBuilder<ApartmentRoomType> builder)
        {
            builder.HasData(
              new ApartmentRoomType[]
              {
                
              });
        }
    }
}
