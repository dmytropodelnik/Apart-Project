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
                  new BedType { Id = 2, Title = "Queen bed", },
                  new BedType { Id = 3, Title = "King bed", },
                  new BedType { Id = 4, Title = "Single bed", },
                  new BedType { Id = 5, Title = "Double bed", },
                  new BedType { Id = 6, Title = "Large bed", },
                  new BedType { Id = 7, Title = "Extra-large double bed", },
                  new BedType { Id = 8, Title = "Bunk bed", },
                  new BedType { Id = 9, Title = "Sofa bed", },
              });
        }
    }
}
