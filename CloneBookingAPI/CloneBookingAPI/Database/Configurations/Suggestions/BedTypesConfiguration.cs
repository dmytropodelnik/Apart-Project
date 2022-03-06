using CloneBookingAPI.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.Suggestions
{
    public class BedTypesConfiguration : IEntityTypeConfiguration<BedType>
    {
        public void Configure(EntityTypeBuilder<BedType> builder)
        {
            builder.HasData(
              new BedType[]
              {
                  new BedType { Id = 1, Title = "Twin beds", },
                  new BedType { Id = 2, Title = "Double bed", },
                  new BedType { Id = 3, Title = "Queen bed", },
                  new BedType { Id = 4, Title = "King bed", },
              });
        }
    }
}
