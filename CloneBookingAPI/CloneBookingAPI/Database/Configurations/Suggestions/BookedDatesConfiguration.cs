using CloneBookingAPI.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.Suggestions
{
    public class BookedDatesConfiguration : IEntityTypeConfiguration<BookedPeriod>
    {
        public void Configure(EntityTypeBuilder<BookedPeriod> builder)
        {
            builder.HasData(
              new BookedPeriod[]
              {
                    
              });
        }
    }
}
