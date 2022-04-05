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
                new ApartmentRoomType { Id = 1, ApartmentId = 1, RoomTypeId = 1, },
                new ApartmentRoomType { Id = 2, ApartmentId = 2, RoomTypeId = 2, },
                new ApartmentRoomType { Id = 3, ApartmentId = 3, RoomTypeId = 3, },
                new ApartmentRoomType { Id = 4, ApartmentId = 4, RoomTypeId = 4, },
                new ApartmentRoomType { Id = 5, ApartmentId = 5, RoomTypeId = 5, },
                new ApartmentRoomType { Id = 6, ApartmentId = 6, RoomTypeId = 6, },
                new ApartmentRoomType { Id = 7, ApartmentId = 7, RoomTypeId = 7, },
                new ApartmentRoomType { Id = 8, ApartmentId = 8, RoomTypeId = 8, },
                new ApartmentRoomType { Id = 9, ApartmentId = 9, RoomTypeId = 9, },
                new ApartmentRoomType { Id = 10, ApartmentId = 10, RoomTypeId = 10, },
                new ApartmentRoomType { Id = 11, ApartmentId = 11, RoomTypeId = 11, },
                new ApartmentRoomType { Id = 12, ApartmentId = 12, RoomTypeId = 12, },
                new ApartmentRoomType { Id = 13, ApartmentId = 13, RoomTypeId = 13, },
                new ApartmentRoomType { Id = 14, ApartmentId = 14, RoomTypeId = 1, },
                new ApartmentRoomType { Id = 15, ApartmentId = 15, RoomTypeId = 2, },
                new ApartmentRoomType { Id = 16, ApartmentId = 16, RoomTypeId = 3, },
                new ApartmentRoomType { Id = 17, ApartmentId = 17, RoomTypeId = 4, },
                new ApartmentRoomType { Id = 18, ApartmentId = 18, RoomTypeId = 5, },
                new ApartmentRoomType { Id = 19, ApartmentId = 19, RoomTypeId = 6, },
                new ApartmentRoomType { Id = 20, ApartmentId = 20, RoomTypeId = 7, },
                new ApartmentRoomType { Id = 21, ApartmentId = 21, RoomTypeId = 8, },
                new ApartmentRoomType { Id = 22, ApartmentId = 22, RoomTypeId = 9, },
                new ApartmentRoomType { Id = 23, ApartmentId = 23, RoomTypeId = 10, },
                new ApartmentRoomType { Id = 24, ApartmentId = 24, RoomTypeId = 11, },
                new ApartmentRoomType { Id = 25, ApartmentId = 25, RoomTypeId = 12, },
                new ApartmentRoomType { Id = 26, ApartmentId = 26, RoomTypeId = 13, },
                new ApartmentRoomType { Id = 27, ApartmentId = 27, RoomTypeId = 1, },
                new ApartmentRoomType { Id = 28, ApartmentId = 28, RoomTypeId = 2, },
                new ApartmentRoomType { Id = 29, ApartmentId = 29, RoomTypeId = 3, },
                new ApartmentRoomType { Id = 30, ApartmentId = 30, RoomTypeId = 4, },
                new ApartmentRoomType { Id = 31, ApartmentId = 31, RoomTypeId = 5, },
                new ApartmentRoomType { Id = 32, ApartmentId = 32, RoomTypeId = 6, },
                new ApartmentRoomType { Id = 33, ApartmentId = 33, RoomTypeId = 7, },
                new ApartmentRoomType { Id = 34, ApartmentId = 34, RoomTypeId = 8, },
                new ApartmentRoomType { Id = 35, ApartmentId = 35, RoomTypeId = 9, },
                new ApartmentRoomType { Id = 36, ApartmentId = 36, RoomTypeId = 10, },
                new ApartmentRoomType { Id = 37, ApartmentId = 37, RoomTypeId = 11, },
                new ApartmentRoomType { Id = 38, ApartmentId = 38, RoomTypeId = 12, },
                new ApartmentRoomType { Id = 39, ApartmentId = 39, RoomTypeId = 13, },
                new ApartmentRoomType { Id = 40, ApartmentId = 40, RoomTypeId = 1, },
                new ApartmentRoomType { Id = 41, ApartmentId = 41, RoomTypeId = 2, },
                new ApartmentRoomType { Id = 42, ApartmentId = 42, RoomTypeId = 3, },
                new ApartmentRoomType { Id = 43, ApartmentId = 43, RoomTypeId = 4, },
                new ApartmentRoomType { Id = 44, ApartmentId = 44, RoomTypeId = 5, },
                new ApartmentRoomType { Id = 45, ApartmentId = 45, RoomTypeId = 6, },
                new ApartmentRoomType { Id = 46, ApartmentId = 46, RoomTypeId = 7, },
                new ApartmentRoomType { Id = 47, ApartmentId = 47, RoomTypeId = 8, },
                new ApartmentRoomType { Id = 48, ApartmentId = 48, RoomTypeId = 9, },
                new ApartmentRoomType { Id = 49, ApartmentId = 49, RoomTypeId = 10, },
                new ApartmentRoomType { Id = 50, ApartmentId = 50, RoomTypeId = 11, },
                new ApartmentRoomType { Id = 51, ApartmentId = 51, RoomTypeId = 12, },
                new ApartmentRoomType { Id = 52, ApartmentId = 52, RoomTypeId = 13, },
                new ApartmentRoomType { Id = 53, ApartmentId = 53, RoomTypeId = 1, },
                new ApartmentRoomType { Id = 54, ApartmentId = 54, RoomTypeId = 2, },
                new ApartmentRoomType { Id = 55, ApartmentId = 55, RoomTypeId = 3, },
                new ApartmentRoomType { Id = 56, ApartmentId = 56, RoomTypeId = 4, },
                new ApartmentRoomType { Id = 57, ApartmentId = 57, RoomTypeId = 5, },
                new ApartmentRoomType { Id = 58, ApartmentId = 58, RoomTypeId = 6, },
                new ApartmentRoomType { Id = 59, ApartmentId = 59, RoomTypeId = 7, },
                new ApartmentRoomType { Id = 60, ApartmentId = 60, RoomTypeId = 8, },
                new ApartmentRoomType { Id = 61, ApartmentId = 61, RoomTypeId = 9, },
                new ApartmentRoomType { Id = 62, ApartmentId = 62, RoomTypeId = 10, },
                new ApartmentRoomType { Id = 63, ApartmentId = 63, RoomTypeId = 11, },
                new ApartmentRoomType { Id = 64, ApartmentId = 64, RoomTypeId = 12, },
                new ApartmentRoomType { Id = 65, ApartmentId = 65, RoomTypeId = 13, },
                new ApartmentRoomType { Id = 66, ApartmentId = 66, RoomTypeId = 1, },
                new ApartmentRoomType { Id = 67, ApartmentId = 67, RoomTypeId = 2, },
                new ApartmentRoomType { Id = 68, ApartmentId = 68, RoomTypeId = 3, },
                new ApartmentRoomType { Id = 69, ApartmentId = 69, RoomTypeId = 4, },
                new ApartmentRoomType { Id = 70, ApartmentId = 70, RoomTypeId = 5, },
              });
        }
    }
}
