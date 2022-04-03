using CloneBookingAPI.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.Suggestions
{
    public class BookedDatesConfiguration : IEntityTypeConfiguration<BookedDate>
    {
        public void Configure(EntityTypeBuilder<BookedDate> builder)
        {
            builder.HasData(
              new BookedDate[]
              {
                    
              });
        }
    }
}
