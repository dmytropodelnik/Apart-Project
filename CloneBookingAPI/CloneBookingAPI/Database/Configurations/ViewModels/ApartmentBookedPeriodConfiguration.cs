using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class ApartmentBookedPeriodConfiguration : IEntityTypeConfiguration<ApartmentBookedPeriod>
    {
        public void Configure(EntityTypeBuilder<ApartmentBookedPeriod> builder)
        {
            builder.HasData(
              new ApartmentBookedPeriod[]
              {
                  new ApartmentBookedPeriod { Id = 1, ApartmentId = 1, BookedPeriodId = 1, },
                  new ApartmentBookedPeriod { Id = 2, ApartmentId = 2, BookedPeriodId = 2, },
                  new ApartmentBookedPeriod { Id = 3, ApartmentId = 3, BookedPeriodId = 3, },
                  new ApartmentBookedPeriod { Id = 4, ApartmentId = 4, BookedPeriodId = 4, },
                  new ApartmentBookedPeriod { Id = 5, ApartmentId = 5, BookedPeriodId = 5, },
                  new ApartmentBookedPeriod { Id = 6, ApartmentId = 6, BookedPeriodId = 6, },
                  new ApartmentBookedPeriod { Id = 7, ApartmentId = 7, BookedPeriodId = 7, },
                  new ApartmentBookedPeriod { Id = 8, ApartmentId = 8, BookedPeriodId = 8, },
                  new ApartmentBookedPeriod { Id = 9, ApartmentId = 9, BookedPeriodId = 9, },
                  new ApartmentBookedPeriod { Id = 10, ApartmentId = 10, BookedPeriodId = 10, },
                  new ApartmentBookedPeriod { Id = 11, ApartmentId = 11, BookedPeriodId = 11, },
                  new ApartmentBookedPeriod { Id = 12, ApartmentId = 12, BookedPeriodId = 12, },
                  new ApartmentBookedPeriod { Id = 13, ApartmentId = 13, BookedPeriodId = 13, },
                  new ApartmentBookedPeriod { Id = 14, ApartmentId = 14, BookedPeriodId = 14, },
                  new ApartmentBookedPeriod { Id = 15, ApartmentId = 15, BookedPeriodId = 15, },
                  new ApartmentBookedPeriod { Id = 16, ApartmentId = 16, BookedPeriodId = 16, },
                  new ApartmentBookedPeriod { Id = 17, ApartmentId = 17, BookedPeriodId = 17, },
                  new ApartmentBookedPeriod { Id = 18, ApartmentId = 18, BookedPeriodId = 18, },
                  new ApartmentBookedPeriod { Id = 19, ApartmentId = 19, BookedPeriodId = 19, },
                  new ApartmentBookedPeriod { Id = 20, ApartmentId = 20, BookedPeriodId = 20, },
                  new ApartmentBookedPeriod { Id = 21, ApartmentId = 21, BookedPeriodId = 21, },
                  new ApartmentBookedPeriod { Id = 22, ApartmentId = 22, BookedPeriodId = 22, },
                  new ApartmentBookedPeriod { Id = 23, ApartmentId = 23, BookedPeriodId = 23, },
                  new ApartmentBookedPeriod { Id = 24, ApartmentId = 24, BookedPeriodId = 24, },
                  new ApartmentBookedPeriod { Id = 25, ApartmentId = 25, BookedPeriodId = 25, },
                  new ApartmentBookedPeriod { Id = 26, ApartmentId = 26, BookedPeriodId = 26, },
                  new ApartmentBookedPeriod { Id = 27, ApartmentId = 27, BookedPeriodId = 27, },
                  new ApartmentBookedPeriod { Id = 28, ApartmentId = 28, BookedPeriodId = 28, },
                  new ApartmentBookedPeriod { Id = 29, ApartmentId = 29, BookedPeriodId = 29, },
                  new ApartmentBookedPeriod { Id = 30, ApartmentId = 30, BookedPeriodId = 30, },
                  new ApartmentBookedPeriod { Id = 31, ApartmentId = 31, BookedPeriodId = 1, },
                  new ApartmentBookedPeriod { Id = 32, ApartmentId = 32, BookedPeriodId = 2, },
                  new ApartmentBookedPeriod { Id = 33, ApartmentId = 33, BookedPeriodId = 3, },
                  new ApartmentBookedPeriod { Id = 34, ApartmentId = 34, BookedPeriodId = 4, },
                  new ApartmentBookedPeriod { Id = 35, ApartmentId = 35, BookedPeriodId = 5, },
                  new ApartmentBookedPeriod { Id = 36, ApartmentId = 36, BookedPeriodId = 6, },
                  new ApartmentBookedPeriod { Id = 37, ApartmentId = 37, BookedPeriodId = 7, },
                  new ApartmentBookedPeriod { Id = 38, ApartmentId = 38, BookedPeriodId = 8, },
                  new ApartmentBookedPeriod { Id = 39, ApartmentId = 39, BookedPeriodId = 9, },
                  new ApartmentBookedPeriod { Id = 40, ApartmentId = 40, BookedPeriodId = 10, },
                  new ApartmentBookedPeriod { Id = 41, ApartmentId = 41, BookedPeriodId = 11, },
                  new ApartmentBookedPeriod { Id = 42, ApartmentId = 42, BookedPeriodId = 12, },
                  new ApartmentBookedPeriod { Id = 43, ApartmentId = 43, BookedPeriodId = 13, },
                  new ApartmentBookedPeriod { Id = 44, ApartmentId = 44, BookedPeriodId = 14, },
                  new ApartmentBookedPeriod { Id = 45, ApartmentId = 45, BookedPeriodId = 15, },
                  new ApartmentBookedPeriod { Id = 46, ApartmentId = 46, BookedPeriodId = 16, },
                  new ApartmentBookedPeriod { Id = 47, ApartmentId = 47, BookedPeriodId = 17, },
                  new ApartmentBookedPeriod { Id = 48, ApartmentId = 48, BookedPeriodId = 18, },
                  new ApartmentBookedPeriod { Id = 49, ApartmentId = 49, BookedPeriodId = 19, },
                  new ApartmentBookedPeriod { Id = 50, ApartmentId = 50, BookedPeriodId = 20, },
                  new ApartmentBookedPeriod { Id = 51, ApartmentId = 51, BookedPeriodId = 21, },
                  new ApartmentBookedPeriod { Id = 52, ApartmentId = 52, BookedPeriodId = 22, },
                  new ApartmentBookedPeriod { Id = 53, ApartmentId = 53, BookedPeriodId = 23, },
                  new ApartmentBookedPeriod { Id = 54, ApartmentId = 54, BookedPeriodId = 24, },
                  new ApartmentBookedPeriod { Id = 55, ApartmentId = 55, BookedPeriodId = 25, },
                  new ApartmentBookedPeriod { Id = 56, ApartmentId = 56, BookedPeriodId = 26, },
                  new ApartmentBookedPeriod { Id = 57, ApartmentId = 57, BookedPeriodId = 27, },
                  new ApartmentBookedPeriod { Id = 58, ApartmentId = 58, BookedPeriodId = 28, },
                  new ApartmentBookedPeriod { Id = 59, ApartmentId = 59, BookedPeriodId = 29, },
                  new ApartmentBookedPeriod { Id = 60, ApartmentId = 60, BookedPeriodId = 30, },
                  new ApartmentBookedPeriod { Id = 61, ApartmentId = 61, BookedPeriodId = 1, },
                  new ApartmentBookedPeriod { Id = 62, ApartmentId = 62, BookedPeriodId = 2, },
                  new ApartmentBookedPeriod { Id = 63, ApartmentId = 63, BookedPeriodId = 3, },
                  new ApartmentBookedPeriod { Id = 64, ApartmentId = 64, BookedPeriodId = 4, },
                  new ApartmentBookedPeriod { Id = 65, ApartmentId = 65, BookedPeriodId = 5, },
                  new ApartmentBookedPeriod { Id = 66, ApartmentId = 66, BookedPeriodId = 6, },
                  new ApartmentBookedPeriod { Id = 67, ApartmentId = 67, BookedPeriodId = 7, },
                  new ApartmentBookedPeriod { Id = 68, ApartmentId = 68, BookedPeriodId = 8, },
                  new ApartmentBookedPeriod { Id = 69, ApartmentId = 69, BookedPeriodId = 9, },
                  new ApartmentBookedPeriod { Id = 70, ApartmentId = 70, BookedPeriodId = 10, },
              });
        }
    }
}
