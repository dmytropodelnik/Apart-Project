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
                  new StayBookingApartment { Id = 1, StayBookingId = 1, ApartmentId = 1 },
                  new StayBookingApartment { Id = 2, StayBookingId = 1, ApartmentId = 2 },
                  new StayBookingApartment { Id = 3, StayBookingId = 1, ApartmentId = 3 },

                  new StayBookingApartment { Id = 4, StayBookingId = 2, ApartmentId = 4 },
                  new StayBookingApartment { Id = 5, StayBookingId = 2, ApartmentId = 5 },
                  new StayBookingApartment { Id = 6, StayBookingId = 2, ApartmentId = 6 },
                  new StayBookingApartment { Id = 7, StayBookingId = 2, ApartmentId = 7 },

                  new StayBookingApartment { Id = 8, StayBookingId = 3, ApartmentId = 8 },
                  new StayBookingApartment { Id = 9, StayBookingId = 3, ApartmentId = 9 },
                  new StayBookingApartment { Id = 10, StayBookingId = 3, ApartmentId = 10 },
                  new StayBookingApartment { Id = 11, StayBookingId = 3, ApartmentId = 11 },
                  new StayBookingApartment { Id = 12, StayBookingId = 3, ApartmentId = 12 },

                  new StayBookingApartment { Id = 13, StayBookingId = 4, ApartmentId = 13 },
                  new StayBookingApartment { Id = 14, StayBookingId = 4, ApartmentId = 14 },
                  new StayBookingApartment { Id = 15, StayBookingId = 4, ApartmentId = 15 },

                  new StayBookingApartment { Id = 16, StayBookingId = 5, ApartmentId = 16 },
                  new StayBookingApartment { Id = 17, StayBookingId = 5, ApartmentId = 17 },

                  new StayBookingApartment { Id = 18, StayBookingId = 6, ApartmentId = 18 },
                  new StayBookingApartment { Id = 19, StayBookingId = 6, ApartmentId = 19 },

                  new StayBookingApartment { Id = 20, StayBookingId = 7, ApartmentId = 20 },
                  new StayBookingApartment { Id = 21, StayBookingId = 7, ApartmentId = 21 },
                  new StayBookingApartment { Id = 22, StayBookingId = 7, ApartmentId = 22 },
                  new StayBookingApartment { Id = 23, StayBookingId = 7, ApartmentId = 23 },

                  new StayBookingApartment { Id = 24, StayBookingId = 8, ApartmentId = 24 },
                  new StayBookingApartment { Id = 25, StayBookingId = 8, ApartmentId = 25 },
                  new StayBookingApartment { Id = 26, StayBookingId = 8, ApartmentId = 26 },

                  new StayBookingApartment { Id = 27, StayBookingId = 9, ApartmentId = 27 },
                  new StayBookingApartment { Id = 28, StayBookingId = 9, ApartmentId = 28 },
                  new StayBookingApartment { Id = 29, StayBookingId = 9, ApartmentId = 29 },
                  new StayBookingApartment { Id = 30, StayBookingId = 9, ApartmentId = 30 },
              });
        }
    }
}
