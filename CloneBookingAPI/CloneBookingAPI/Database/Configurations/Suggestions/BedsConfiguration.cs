using CloneBookingAPI.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.Suggestions
{
    public class BedsConfiguration : IEntityTypeConfiguration<Bed>
    {
        public void Configure(EntityTypeBuilder<Bed> builder)
        {
            builder.HasData(
              new Bed[]
              {
                  new Bed { Id = 1, Description = "test1", BedTypeId = 1, },
                  new Bed { Id = 2, Description = "test2", BedTypeId = 2, },
                  new Bed { Id = 3, Description = "test3", BedTypeId = 3, },
                  new Bed { Id = 4, Description = "test4", BedTypeId = 4, },
              });
        }
    }
}
