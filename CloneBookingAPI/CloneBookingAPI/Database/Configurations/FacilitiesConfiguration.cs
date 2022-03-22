using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class FacilitiesConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.HasData(
              new Facility[]
              {
                new Facility { Id = 1, Text = "Beachfront", FacilityTypeId = 2, ImageId = 1 },
                new Facility { Id = 2, Text = "Private beach area", FacilityTypeId = 2, ImageId = 1 },
                new Facility { Id = 3, Text = "Terrace", FacilityTypeId = 2, ImageId = 1 },
                new Facility { Id = 4, Text = "Garden", FacilityTypeId = 2, ImageId = 1 },
                new Facility { Id = 5, Text = "Babysitting/Child services", FacilityTypeId = 3, ImageId = 1 },
                new Facility { Id = 6, Text = "Open all year", FacilityTypeId = 4, ImageId = 1 },
                new Facility { Id = 7, Text = "All ages welcome", FacilityTypeId = 4, ImageId = 1 },
                new Facility { Id = 8, Text = "Beach", FacilityTypeId = 5, ImageId = 1 },
                new Facility { Id = 9, Text = "Kids' club", FacilityTypeId = 5, ImageId = 1 },
                new Facility { Id = 10, Text = "Snorkeling", FacilityTypeId = 5, ImageId = 1 },
                new Facility { Id = 11, Text = "Diving", FacilityTypeId = 5, ImageId = 1 },
                new Facility { Id = 12, Text = "Playground", FacilityTypeId = 5, ImageId = 1 },
                new Facility { Id = 13, Text = "Game room", FacilityTypeId = 5, ImageId = 1 },
                new Facility { Id = 14, Text = "Daily housekeeping", FacilityTypeId = 6, ImageId = 1 },
                new Facility { Id = 15, Text = "Ironing service", FacilityTypeId = 6, ImageId = 1 },
                new Facility { Id = 16, Text = "Dry cleaning", FacilityTypeId = 6, ImageId = 1 },
                new Facility { Id = 17, Text = "Laundry", FacilityTypeId = 6, ImageId = 1 },
                new Facility { Id = 18, Text = "Fitness", FacilityTypeId = 7, ImageId = 1 },
                new Facility { Id = 19, Text = "Spa facilities", FacilityTypeId = 7, ImageId = 1 },
                new Facility { Id = 20, Text = "Beach umbrellas", FacilityTypeId = 7, ImageId = 1 },
                new Facility { Id = 21, Text = "Turkish/Steam Bath", FacilityTypeId = 7, ImageId = 1 },
                new Facility { Id = 22, Text = "Massage", FacilityTypeId = 7, ImageId = 1 },
                new Facility { Id = 23, Text = "Spa", FacilityTypeId = 7, ImageId = 1 },
                new Facility { Id = 24, Text = "Fitness center", FacilityTypeId = 7, ImageId = 1 },
                new Facility { Id = 25, Text = "Sauna", FacilityTypeId = 7, ImageId = 1 },
                new Facility { Id = 26, Text = "Fax/Photocopying", FacilityTypeId = 8, ImageId = 1 },
                new Facility { Id = 27, Text = "Meeting/Banquet facilities", FacilityTypeId = 8, ImageId = 1 },
                new Facility { Id = 28, Text = "Smoke alarms", FacilityTypeId = 9, ImageId = 1 },
                new Facility { Id = 29, Text = "24-hour security", FacilityTypeId = 9, ImageId = 1 },
                new Facility { Id = 30, Text = "Safe", FacilityTypeId = 9, ImageId = 1 },
                new Facility { Id = 31, Text = "Breakfast in the room", FacilityTypeId = 10, ImageId = 1 },
                new Facility { Id = 32, Text = "Bar", FacilityTypeId = 10, ImageId = 1 },
                new Facility { Id = 36, Text = "Free public parking is available on site.", FacilityTypeId = 12, ImageId = 1 },
                new Facility { Id = 37, Text = "Street parking", FacilityTypeId = 12, ImageId = 1 },
                new Facility { Id = 38, Text = "WiFi is available in all areas and is free of charge.", FacilityTypeId = 13, ImageId = 1 },
                new Facility { Id = 39, Text = "Restaurant", FacilityTypeId = 13, ImageId = 1 },
                new Facility { Id = 41, Text = "Baggage storage", FacilityTypeId = 14, ImageId = 1 },
                new Facility { Id = 42, Text = "Tour desk", FacilityTypeId = 14, ImageId = 1 },
                new Facility { Id = 43, Text = "Currency exchange", FacilityTypeId = 14, ImageId = 1 },
                new Facility { Id = 44, Text = "24-hour front desk", FacilityTypeId = 14, ImageId = 1 },
                new Facility { Id = 45, Text = "Designated smoking area", FacilityTypeId = 15, ImageId = 1 },
                new Facility { Id = 46, Text = "Air conditioning", FacilityTypeId = 15, ImageId = 1 },
                new Facility { Id = 47, Text = "Heating", FacilityTypeId = 15, ImageId = 1 },
                new Facility { Id = 48, Text = "Chapel/Shrine", FacilityTypeId = 15, ImageId = 1 },
                new Facility { Id = 49, Text = "Elevator", FacilityTypeId = 15, ImageId = 1 },
                new Facility { Id = 50, Text = "Family rooms", FacilityTypeId = 15, ImageId = 1 },
                new Facility { Id = 51, Text = "Facilities for disabled guests", FacilityTypeId = 15, ImageId = 1 },
                new Facility { Id = 52, Text = "Non-smoking rooms", FacilityTypeId = 15, ImageId = 1 },
                new Facility { Id = 53, Text = "Room service", FacilityTypeId = 15, ImageId = 1 },
              });
        }
    }
}
