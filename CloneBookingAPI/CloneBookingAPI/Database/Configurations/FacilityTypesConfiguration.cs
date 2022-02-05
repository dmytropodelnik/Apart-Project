using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class FacilityTypesConfiguration : IEntityTypeConfiguration<FacilityType>
    {
        public void Configure(EntityTypeBuilder<FacilityType> builder)
        {
            builder.HasData(
              new FacilityType[]
              {
                  new FacilityType { Id = 1, Type = "Most popular facilities", },
                  new FacilityType { Id = 2, Type = "Outdoors", },
                  new FacilityType { Id = 3, Type = "Entertainment & Family Services", },
                  new FacilityType { Id = 4, Type = "Outdoor swimming pool", },
                  new FacilityType { Id = 5, Type = "Activities", },
                  new FacilityType { Id = 6, Type = "Cleaning Services", },
                  new FacilityType { Id = 7, Type = "Spa", },
                  new FacilityType { Id = 8, Type = "Business Facilities", },
                  new FacilityType { Id = 9, Type = "Safety & security", },
                  new FacilityType { Id = 10, Type = "Food & Drink", },
                  new FacilityType { Id = 11, Type = "Languages Spoken", },
                  new FacilityType { Id = 12, Type = "Parking", },
                  new FacilityType { Id = 13, Type = "Internet", },
                  new FacilityType { Id = 14, Type = "Front Desk Services", },
                  new FacilityType { Id = 15, Type = "General", },
              });
        }
    }
}
