using CloneBookingAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations
{
    public class InterestPlacesConfiguration : IEntityTypeConfiguration<InterestPlace>
    {
        public void Configure(EntityTypeBuilder<InterestPlace> builder)
        {
            builder.HasData(
              new InterestPlace[]
              {
                  new InterestPlace { Id = 1, Place = "Hotels", },
                  new InterestPlace { Id = 2, Place = "Places of interest", },
                  new InterestPlace { Id = 3, Place = "Homes", },
                  new InterestPlace { Id = 4, Place = "Apartments", },
                  new InterestPlace { Id = 5, Place = "Resorts", },
                  new InterestPlace { Id = 6, Place = "Villas", },
                  new InterestPlace { Id = 7, Place = "Hostels", },
                  new InterestPlace { Id = 8, Place = "B&Bs", },
                  new InterestPlace { Id = 9, Place = "Guest houses", },
                  new InterestPlace { Id = 10, Place = "Unique places to stay", },
                  new InterestPlace { Id = 11, Place = "Vacation Homes", },
                  new InterestPlace { Id = 12, Place = "Serviced Apartments", },
                  new InterestPlace { Id = 13, Place = "Glamping", },
                  new InterestPlace { Id = 14, Place = "Cottages", },
                  new InterestPlace { Id = 15, Place = "Cabins", },
                  new InterestPlace { Id = 16, Place = "Motels", },
                  new InterestPlace { Id = 17, Place = "Ryokans", },
                  new InterestPlace { Id = 18, Place = "Riads", },
                  new InterestPlace { Id = 19, Place = "Resort Villages", },
                  new InterestPlace { Id = 20, Place = "Homestays", },
                  new InterestPlace { Id = 21, Place = "Campgrounds", },
                  new InterestPlace { Id = 22, Place = "Country Houses", },
                  new InterestPlace { Id = 23, Place = "Farm Stays", },
                  new InterestPlace { Id = 24, Place = "Boats", },
                  new InterestPlace { Id = 25, Place = "Luxury Tents" },
                  new InterestPlace { Id = 26, Place = "Self-catering Accommodations", },
                  new InterestPlace { Id = 27, Place = "Tiny houses", },
              });
        }
    }
}
