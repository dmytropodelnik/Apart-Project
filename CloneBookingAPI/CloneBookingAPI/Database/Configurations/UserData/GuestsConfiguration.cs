using CloneBookingAPI.Services.Database.Models.UserData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.UserData
{
    public class GuestsConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.HasData(
              new Guest[]
              {
                  new Guest { Id = 1, FullName = "Dmytro Podelnik", },
                  new Guest { Id = 2, FullName = "Igor Podelnik", },
                  new Guest { Id = 3, FullName = "Dmytro Almazov", },
                  new Guest { Id = 4, FullName = "Heider Alkateb", },
                  new Guest { Id = 5, FullName = "Andrew Podelnik", },
                  new Guest { Id = 6, FullName = "Ivan Podelnik", },
                  new Guest { Id = 7, FullName = "Alex Podelnik", },
                  new Guest { Id = 8, FullName = "Dmytro Petrov", },
                  new Guest { Id = 9, FullName = "Alexander Ivanov", },
                  new Guest { Id = 10, FullName = "Vadim Smith", },
                  new Guest { Id = 11, FullName = "John Podelnik", },
                  new Guest { Id = 12, FullName = "John Smith", },
                  new Guest { Id = 13, FullName = "Tom Cruise", },
                  new Guest { Id = 14, FullName = "Tom Hardi", },
                  new Guest { Id = 15, FullName = "Jason Statham", },
                  new Guest { Id = 16, FullName = "Tatyana Podelnik", },
                  new Guest { Id = 17, FullName = "Ivan Suvorova", },
                  new Guest { Id = 18, FullName = "Julia Suvorova", },
                  new Guest { Id = 19, FullName = "Natali Tepol", },
              });
        }
    }
}
