using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Suggestions
{
    public class RoomTypesConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasData(
              new RoomType[]
              {
                  new RoomType { Id = 1, Title = "Small Double Room", },
                  new RoomType { Id = 2, Title = "Standard Double Room", },
                  new RoomType { Id = 3, Title = "Standard Twin Room", },
                  new RoomType { Id = 4, Title = "Superior Twin Room", },
                  new RoomType { Id = 5, Title = "Studio", },
                  new RoomType { Id = 6, Title = "Standard Quadruple Room", },
                  new RoomType { Id = 7, Title = "Apartment", },
                  new RoomType { Id = 8, Title = "Double Room with Balcony", },
                  new RoomType { Id = 9, Title = "Double Room with Extra Bed", },
                  new RoomType { Id = 10, Title = "Contempo Double Room", },
                  new RoomType { Id = 11, Title = "Double Room", },
                  new RoomType { Id = 12, Title = "Deluxe Double Room", },
                  new RoomType { Id = 13, Title = "Classic Suite", },
              });
        }
    }
}
