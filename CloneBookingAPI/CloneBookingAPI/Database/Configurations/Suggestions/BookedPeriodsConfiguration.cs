using CloneBookingAPI.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloneBookingAPI.Database.Configurations.Suggestions
{
    public class BookedPeriodsConfiguration : IEntityTypeConfiguration<BookedPeriod>
    {
        public void Configure(EntityTypeBuilder<BookedPeriod> builder)
        {
            builder.HasData(
              new BookedPeriod[]
              {
                    new BookedPeriod { Id = 1, DateIn = new DateTime(2022, 4, 1), DateOut = new DateTime(2022, 4, 5), },
                    new BookedPeriod { Id = 2, DateIn = new DateTime(2022, 3, 25), DateOut = new DateTime(2022, 4, 3), },
                    new BookedPeriod { Id = 3, DateIn = new DateTime(2022, 4, 28), DateOut = new DateTime(2022, 5, 10), },
                    new BookedPeriod { Id = 4, DateIn = new DateTime(2022, 4, 29), DateOut = new DateTime(2022, 5, 20), },
                    new BookedPeriod { Id = 5, DateIn = new DateTime(2022, 5, 1), DateOut = new DateTime(2022, 5, 29), },
                    new BookedPeriod { Id = 6, DateIn = new DateTime(2022, 5, 15), DateOut = new DateTime(2022, 5, 27), },
                    new BookedPeriod { Id = 7, DateIn = new DateTime(2022, 6, 15), DateOut = new DateTime(2022, 6, 21), },
                    new BookedPeriod { Id = 8, DateIn = new DateTime(2022, 7, 5), DateOut = new DateTime(2022, 8, 2), },
                    new BookedPeriod { Id = 9, DateIn = new DateTime(2022, 7, 1), DateOut = new DateTime(2022, 7, 16), },
                    new BookedPeriod { Id = 10, DateIn = new DateTime(2022, 7, 2), DateOut = new DateTime(2022, 7, 9), },
                    new BookedPeriod { Id = 11, DateIn = new DateTime(2022, 9, 1), DateOut = new DateTime(2022, 9, 27), },
                    new BookedPeriod { Id = 12, DateIn = new DateTime(2022, 9, 15), DateOut = new DateTime(2022, 9, 21), },
                    new BookedPeriod { Id = 13, DateIn = new DateTime(2022, 9, 5), DateOut = new DateTime(2022, 9, 12), },
                    new BookedPeriod { Id = 14, DateIn = new DateTime(2022, 9, 3), DateOut = new DateTime(2022, 9, 16), },
                    new BookedPeriod { Id = 15, DateIn = new DateTime(2022, 10, 2), DateOut = new DateTime(2022, 10, 9), },
                    new BookedPeriod { Id = 16, DateIn = new DateTime(2022, 11, 15), DateOut = new DateTime(2022, 11, 27), },
                    new BookedPeriod { Id = 17, DateIn = new DateTime(2022, 11, 17), DateOut = new DateTime(2022, 11, 24), },
                    new BookedPeriod { Id = 18, DateIn = new DateTime(2022, 11, 22), DateOut = new DateTime(2022, 11, 30), },
                    new BookedPeriod { Id = 19, DateIn = new DateTime(2022, 12, 1), DateOut = new DateTime(2022, 12, 17), },
                    new BookedPeriod { Id = 20, DateIn = new DateTime(2022, 12, 18), DateOut = new DateTime(2022, 12, 27), },
                    new BookedPeriod { Id = 21, DateIn = new DateTime(2023, 1, 2), DateOut = new DateTime(2023, 7, 9), },
                    new BookedPeriod { Id = 22, DateIn = new DateTime(2023, 9, 1), DateOut = new DateTime(2023, 9, 27), },
                    new BookedPeriod { Id = 23, DateIn = new DateTime(2023, 9, 15), DateOut = new DateTime(2023, 9, 21), },
                    new BookedPeriod { Id = 24, DateIn = new DateTime(2023, 9, 5), DateOut = new DateTime(2023, 9, 12), },
                    new BookedPeriod { Id = 25, DateIn = new DateTime(2023, 9, 3), DateOut = new DateTime(2023, 9, 16), },
                    new BookedPeriod { Id = 26, DateIn = new DateTime(2023, 10, 2), DateOut = new DateTime(2023, 10, 9), },
                    new BookedPeriod { Id = 27, DateIn = new DateTime(2023, 11, 15), DateOut = new DateTime(2023, 11, 27), },
                    new BookedPeriod { Id = 28, DateIn = new DateTime(2023, 11, 17), DateOut = new DateTime(2023, 11, 24), },
                    new BookedPeriod { Id = 29, DateIn = new DateTime(2023, 11, 22), DateOut = new DateTime(2023, 11, 30), },
                    new BookedPeriod { Id = 30, DateIn = new DateTime(2023, 12, 1), DateOut = new DateTime(2023, 12, 17), },
              });
        }
    }
}
