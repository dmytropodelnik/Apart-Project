﻿using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class SuggestionsFacilitiesConfiguration : IEntityTypeConfiguration<SuggestionFacility>
    {
        public void Configure(EntityTypeBuilder<SuggestionFacility> builder)
        {
            builder.HasData(
              new SuggestionFacility[]
              {
                  new SuggestionFacility { Id = 1, FacilityId = 1, SuggestionId = 1, },
                  new SuggestionFacility { Id = 2, FacilityId = 2, SuggestionId = 1, },
                  new SuggestionFacility { Id = 3, FacilityId = 3, SuggestionId = 1, },
                  new SuggestionFacility { Id = 4, FacilityId = 4, SuggestionId = 1, },
                  new SuggestionFacility { Id = 5, FacilityId = 5, SuggestionId = 1, },
                  new SuggestionFacility { Id = 6, FacilityId = 6, SuggestionId = 1, },
                  new SuggestionFacility { Id = 7, FacilityId = 7, SuggestionId = 1, },
                  new SuggestionFacility { Id = 8, FacilityId = 8, SuggestionId = 1, },
                  new SuggestionFacility { Id = 9, FacilityId = 9, SuggestionId = 1, },
                  new SuggestionFacility { Id = 10, FacilityId = 10, SuggestionId = 1, },
                  new SuggestionFacility { Id = 11, FacilityId = 11, SuggestionId = 1, },
                  new SuggestionFacility { Id = 12, FacilityId = 12, SuggestionId = 1, },
                  new SuggestionFacility { Id = 13, FacilityId = 13, SuggestionId = 1, },
                  new SuggestionFacility { Id = 14, FacilityId = 14, SuggestionId = 1, },
                  new SuggestionFacility { Id = 15, FacilityId = 15, SuggestionId = 1, },
                  new SuggestionFacility { Id = 16, FacilityId = 16, SuggestionId = 1, },
                  new SuggestionFacility { Id = 17, FacilityId = 17, SuggestionId = 1, },
                  new SuggestionFacility { Id = 18, FacilityId = 18, SuggestionId = 1, },
                  new SuggestionFacility { Id = 19, FacilityId = 19, SuggestionId = 1, },
                  new SuggestionFacility { Id = 20, FacilityId = 20, SuggestionId = 1, },
                  new SuggestionFacility { Id = 21, FacilityId = 21, SuggestionId = 1, },
                  new SuggestionFacility { Id = 22, FacilityId = 22, SuggestionId = 1, },
                  new SuggestionFacility { Id = 23, FacilityId = 23, SuggestionId = 1, },
                  new SuggestionFacility { Id = 24, FacilityId = 24, SuggestionId = 1, },
                  new SuggestionFacility { Id = 25, FacilityId = 25, SuggestionId = 1, },

                  new SuggestionFacility { Id = 26, FacilityId = 1, SuggestionId = 15, },
                  new SuggestionFacility { Id = 27, FacilityId = 2, SuggestionId = 15, },
                  new SuggestionFacility { Id = 28, FacilityId = 3, SuggestionId = 15, },
                  new SuggestionFacility { Id = 29, FacilityId = 4, SuggestionId = 15, },
                  new SuggestionFacility { Id = 30, FacilityId = 5, SuggestionId = 15, },
                  new SuggestionFacility { Id = 31, FacilityId = 6, SuggestionId = 15, },
                  new SuggestionFacility { Id = 32, FacilityId = 7, SuggestionId = 15, },
                  new SuggestionFacility { Id = 33, FacilityId = 8, SuggestionId = 15, },
                  new SuggestionFacility { Id = 34, FacilityId = 9, SuggestionId = 15, },
                  new SuggestionFacility { Id = 35, FacilityId = 10, SuggestionId = 15, },
                  new SuggestionFacility { Id = 36, FacilityId = 11, SuggestionId = 15, },
                  new SuggestionFacility { Id = 37, FacilityId = 12, SuggestionId = 15, },
                  new SuggestionFacility { Id = 38, FacilityId = 13, SuggestionId = 15, },
                  new SuggestionFacility { Id = 39, FacilityId = 14, SuggestionId = 15, },
                  new SuggestionFacility { Id = 40, FacilityId = 15, SuggestionId = 15, },
                  new SuggestionFacility { Id = 41, FacilityId = 16, SuggestionId = 15, },
                  new SuggestionFacility { Id = 42, FacilityId = 17, SuggestionId = 15, },
                  new SuggestionFacility { Id = 43, FacilityId = 18, SuggestionId = 15, },
                  new SuggestionFacility { Id = 44, FacilityId = 19, SuggestionId = 15, },
                  new SuggestionFacility { Id = 45, FacilityId = 20, SuggestionId = 15, },
                  new SuggestionFacility { Id = 46, FacilityId = 21, SuggestionId = 15, },
                  new SuggestionFacility { Id = 47, FacilityId = 22, SuggestionId = 15, },
                  new SuggestionFacility { Id = 48, FacilityId = 23, SuggestionId = 15, },
                  new SuggestionFacility { Id = 49, FacilityId = 24, SuggestionId = 15, },
                  new SuggestionFacility { Id = 50, FacilityId = 25, SuggestionId = 15, },
                  new SuggestionFacility { Id = 51, FacilityId = 26, SuggestionId = 15, },
                  new SuggestionFacility { Id = 52, FacilityId = 27, SuggestionId = 15, },
                  new SuggestionFacility { Id = 53, FacilityId = 28, SuggestionId = 15, },
                  new SuggestionFacility { Id = 54, FacilityId = 29, SuggestionId = 15, },
                  new SuggestionFacility { Id = 55, FacilityId = 30, SuggestionId = 15, },
                  new SuggestionFacility { Id = 56, FacilityId = 31, SuggestionId = 15, },

                  new SuggestionFacility { Id = 57, FacilityId = 1, SuggestionId = 27, },
                  new SuggestionFacility { Id = 58, FacilityId = 2, SuggestionId = 27, },
                  new SuggestionFacility { Id = 59, FacilityId = 3, SuggestionId = 27, },
                  new SuggestionFacility { Id = 60, FacilityId = 4, SuggestionId = 27, },
                  new SuggestionFacility { Id = 61, FacilityId = 5, SuggestionId = 27, },
                  new SuggestionFacility { Id = 62, FacilityId = 6, SuggestionId = 27, },
                  new SuggestionFacility { Id = 63, FacilityId = 7, SuggestionId = 27, },
                  new SuggestionFacility { Id = 64, FacilityId = 8, SuggestionId = 27, },
                  new SuggestionFacility { Id = 65, FacilityId = 9, SuggestionId = 27, },
                  new SuggestionFacility { Id = 66, FacilityId = 10, SuggestionId = 27, },
                  new SuggestionFacility { Id = 67, FacilityId = 11, SuggestionId = 27, },
                  new SuggestionFacility { Id = 68, FacilityId = 12, SuggestionId = 27, },
                  new SuggestionFacility { Id = 69, FacilityId = 13, SuggestionId = 27, },
                  new SuggestionFacility { Id = 70, FacilityId = 14, SuggestionId = 27, },
                  new SuggestionFacility { Id = 71, FacilityId = 15, SuggestionId = 27, },
                  new SuggestionFacility { Id = 72, FacilityId = 16, SuggestionId = 27, },
                  new SuggestionFacility { Id = 73, FacilityId = 17, SuggestionId = 27, },
                  new SuggestionFacility { Id = 74, FacilityId = 18, SuggestionId = 27, },
                  new SuggestionFacility { Id = 75, FacilityId = 19, SuggestionId = 27, },
                  new SuggestionFacility { Id = 76, FacilityId = 20, SuggestionId = 27, },
                  new SuggestionFacility { Id = 77, FacilityId = 30, SuggestionId = 27, },
                  new SuggestionFacility { Id = 78, FacilityId = 31, SuggestionId = 27, },
                  new SuggestionFacility { Id = 79, FacilityId = 32, SuggestionId = 27, },
                  new SuggestionFacility { Id = 80, FacilityId = 33, SuggestionId = 27, },
                  new SuggestionFacility { Id = 81, FacilityId = 34, SuggestionId = 27, },
                  new SuggestionFacility { Id = 82, FacilityId = 35, SuggestionId = 27, },
                  new SuggestionFacility { Id = 83, FacilityId = 36, SuggestionId = 27, },
                  new SuggestionFacility { Id = 84, FacilityId = 37, SuggestionId = 27, },
                  new SuggestionFacility { Id = 85, FacilityId = 38, SuggestionId = 27, },
                  new SuggestionFacility { Id = 86, FacilityId = 39, SuggestionId = 27, },
                  new SuggestionFacility { Id = 87, FacilityId = 40, SuggestionId = 27, },
                  new SuggestionFacility { Id = 88, FacilityId = 41, SuggestionId = 27, },
                  new SuggestionFacility { Id = 89, FacilityId = 42, SuggestionId = 27, },
                  new SuggestionFacility { Id = 90, FacilityId = 43, SuggestionId = 27, },
                  new SuggestionFacility { Id = 91, FacilityId = 44, SuggestionId = 27, },
                  new SuggestionFacility { Id = 92, FacilityId = 45, SuggestionId = 27, },
                  new SuggestionFacility { Id = 93, FacilityId = 46, SuggestionId = 27, },
                  new SuggestionFacility { Id = 94, FacilityId = 47, SuggestionId = 27, },

                  new SuggestionFacility { Id = 95, FacilityId = 1, SuggestionId = 57, },
                  new SuggestionFacility { Id = 96, FacilityId = 2, SuggestionId = 57, },
                  new SuggestionFacility { Id = 97, FacilityId = 3, SuggestionId = 57, },
                  new SuggestionFacility { Id = 98, FacilityId = 4, SuggestionId = 57, },
                  new SuggestionFacility { Id = 99, FacilityId = 5, SuggestionId = 57, },
                  new SuggestionFacility { Id = 100, FacilityId = 6, SuggestionId = 57, },
                  new SuggestionFacility { Id = 101, FacilityId = 7, SuggestionId = 57, },
                  new SuggestionFacility { Id = 102, FacilityId = 8, SuggestionId = 57, },
                  new SuggestionFacility { Id = 103, FacilityId = 9, SuggestionId = 57, },
                  new SuggestionFacility { Id = 104, FacilityId = 10, SuggestionId = 57, },
                  new SuggestionFacility { Id = 105, FacilityId = 11, SuggestionId = 57, },
                  new SuggestionFacility { Id = 106, FacilityId = 12, SuggestionId = 57, },
                  new SuggestionFacility { Id = 107, FacilityId = 13, SuggestionId = 57, },
                  new SuggestionFacility { Id = 108, FacilityId = 14, SuggestionId = 57, },
                  new SuggestionFacility { Id = 109, FacilityId = 15, SuggestionId = 57, },
                  new SuggestionFacility { Id = 110, FacilityId = 16, SuggestionId = 57, },
                  new SuggestionFacility { Id = 111, FacilityId = 17, SuggestionId = 57, },
                  new SuggestionFacility { Id = 112, FacilityId = 18, SuggestionId = 57, },
                  new SuggestionFacility { Id = 113, FacilityId = 19, SuggestionId = 57, },

                  new SuggestionFacility { Id = 114, FacilityId = 20, SuggestionId = 101, },
                  new SuggestionFacility { Id = 115, FacilityId = 30, SuggestionId = 101, },
                  new SuggestionFacility { Id = 116, FacilityId = 31, SuggestionId = 101, },
                  new SuggestionFacility { Id = 117, FacilityId = 32, SuggestionId = 101, },
                  new SuggestionFacility { Id = 118, FacilityId = 33, SuggestionId = 101, },
                  new SuggestionFacility { Id = 119, FacilityId = 34, SuggestionId = 101, },
                  new SuggestionFacility { Id = 120, FacilityId = 35, SuggestionId = 101, },
                  new SuggestionFacility { Id = 121, FacilityId = 36, SuggestionId = 101, },
                  new SuggestionFacility { Id = 122, FacilityId = 37, SuggestionId = 101, },
                  new SuggestionFacility { Id = 123, FacilityId = 38, SuggestionId = 101, },
                  new SuggestionFacility { Id = 124, FacilityId = 39, SuggestionId = 101, },
                  new SuggestionFacility { Id = 125, FacilityId = 40, SuggestionId = 101, },
                  new SuggestionFacility { Id = 126, FacilityId = 41, SuggestionId = 101, },
                  new SuggestionFacility { Id = 127, FacilityId = 42, SuggestionId = 101, },
                  new SuggestionFacility { Id = 128, FacilityId = 43, SuggestionId = 101, },
                  new SuggestionFacility { Id = 129, FacilityId = 44, SuggestionId = 101, },
                  new SuggestionFacility { Id = 130, FacilityId = 45, SuggestionId = 101, },
                  new SuggestionFacility { Id = 131, FacilityId = 46, SuggestionId = 101, },
                  new SuggestionFacility { Id = 132, FacilityId = 47, SuggestionId = 101, },

                  new SuggestionFacility { Id = 133, FacilityId = 20, SuggestionId = 28, },
                  new SuggestionFacility { Id = 134, FacilityId = 30, SuggestionId = 28, },
                  new SuggestionFacility { Id = 135, FacilityId = 31, SuggestionId = 28, },
                  new SuggestionFacility { Id = 136, FacilityId = 32, SuggestionId = 28, },
                  new SuggestionFacility { Id = 137, FacilityId = 33, SuggestionId = 28, },
                  new SuggestionFacility { Id = 138, FacilityId = 34, SuggestionId = 28, },
                  new SuggestionFacility { Id = 139, FacilityId = 35, SuggestionId = 28, },
                  new SuggestionFacility { Id = 140, FacilityId = 36, SuggestionId = 28, },
                  new SuggestionFacility { Id = 141, FacilityId = 37, SuggestionId = 28, },
                  new SuggestionFacility { Id = 142, FacilityId = 38, SuggestionId = 28, },
                  new SuggestionFacility { Id = 143, FacilityId = 39, SuggestionId = 28, },
                  new SuggestionFacility { Id = 144, FacilityId = 40, SuggestionId = 28, },
                  new SuggestionFacility { Id = 145, FacilityId = 41, SuggestionId = 28, },
                  new SuggestionFacility { Id = 146, FacilityId = 42, SuggestionId = 28, },
                  new SuggestionFacility { Id = 147, FacilityId = 43, SuggestionId = 28, },
                  new SuggestionFacility { Id = 148, FacilityId = 44, SuggestionId = 28, },
                  new SuggestionFacility { Id = 149, FacilityId = 45, SuggestionId = 28, },
                  new SuggestionFacility { Id = 150, FacilityId = 46, SuggestionId = 28, },
                  new SuggestionFacility { Id = 151, FacilityId = 47, SuggestionId = 28, },
                  new SuggestionFacility { Id = 152, FacilityId = 1, SuggestionId = 28, },
                  new SuggestionFacility { Id = 153, FacilityId = 2, SuggestionId = 28, },
                  new SuggestionFacility { Id = 154, FacilityId = 3, SuggestionId = 28, },
                  new SuggestionFacility { Id = 155, FacilityId = 4, SuggestionId = 28, },
                  new SuggestionFacility { Id = 156, FacilityId = 5, SuggestionId = 28, },
                  new SuggestionFacility { Id = 157, FacilityId = 6, SuggestionId = 28, },
                  new SuggestionFacility { Id = 158, FacilityId = 7, SuggestionId = 28, },

                  new SuggestionFacility { Id = 159, FacilityId = 20, SuggestionId = 23, },
                  new SuggestionFacility { Id = 160, FacilityId = 30, SuggestionId = 23, },
                  new SuggestionFacility { Id = 161, FacilityId = 31, SuggestionId = 23, },
                  new SuggestionFacility { Id = 162, FacilityId = 32, SuggestionId = 23, },
                  new SuggestionFacility { Id = 163, FacilityId = 33, SuggestionId = 23, },
                  new SuggestionFacility { Id = 164, FacilityId = 34, SuggestionId = 23, },
                  new SuggestionFacility { Id = 165, FacilityId = 35, SuggestionId = 23, },
                  new SuggestionFacility { Id = 166, FacilityId = 36, SuggestionId = 23, },
                  new SuggestionFacility { Id = 167, FacilityId = 37, SuggestionId = 23, },
                  new SuggestionFacility { Id = 168, FacilityId = 38, SuggestionId = 23, },
                  new SuggestionFacility { Id = 169, FacilityId = 39, SuggestionId = 23, },
                  new SuggestionFacility { Id = 170, FacilityId = 40, SuggestionId = 23, },
                  new SuggestionFacility { Id = 171, FacilityId = 41, SuggestionId = 23, },
                  new SuggestionFacility { Id = 172, FacilityId = 42, SuggestionId = 23, },
                  new SuggestionFacility { Id = 173, FacilityId = 43, SuggestionId = 23, },
                  new SuggestionFacility { Id = 174, FacilityId = 44, SuggestionId = 23, },
                  new SuggestionFacility { Id = 175, FacilityId = 45, SuggestionId = 23, },
                  new SuggestionFacility { Id = 176, FacilityId = 46, SuggestionId = 23, },
                  new SuggestionFacility { Id = 177, FacilityId = 47, SuggestionId = 23, },
                  new SuggestionFacility { Id = 178, FacilityId = 1, SuggestionId = 23, },
                  new SuggestionFacility { Id = 179, FacilityId = 2, SuggestionId = 23, },
                  new SuggestionFacility { Id = 180, FacilityId = 3, SuggestionId = 23, },
                  new SuggestionFacility { Id = 181, FacilityId = 4, SuggestionId = 23, },
                  new SuggestionFacility { Id = 182, FacilityId = 5, SuggestionId = 23, },
                  new SuggestionFacility { Id = 183, FacilityId = 6, SuggestionId = 23, },
                  new SuggestionFacility { Id = 184, FacilityId = 7, SuggestionId = 23, },

                  new SuggestionFacility { Id = 185, FacilityId = 20, SuggestionId = 103, },
                  new SuggestionFacility { Id = 186, FacilityId = 30, SuggestionId = 103, },
                  new SuggestionFacility { Id = 187, FacilityId = 31, SuggestionId = 103, },
                  new SuggestionFacility { Id = 188, FacilityId = 32, SuggestionId = 103, },
                  new SuggestionFacility { Id = 189, FacilityId = 33, SuggestionId = 103, },
                  new SuggestionFacility { Id = 190, FacilityId = 34, SuggestionId = 103, },
                  new SuggestionFacility { Id = 191, FacilityId = 35, SuggestionId = 103, },
                  new SuggestionFacility { Id = 192, FacilityId = 36, SuggestionId = 103, },
                  new SuggestionFacility { Id = 193, FacilityId = 37, SuggestionId = 103, },
                  new SuggestionFacility { Id = 194, FacilityId = 38, SuggestionId = 103, },
                  new SuggestionFacility { Id = 195, FacilityId = 39, SuggestionId = 103, },
                  new SuggestionFacility { Id = 196, FacilityId = 40, SuggestionId = 103, },
                  new SuggestionFacility { Id = 197, FacilityId = 41, SuggestionId = 103, },
                  new SuggestionFacility { Id = 198, FacilityId = 42, SuggestionId = 103, },
                  new SuggestionFacility { Id = 199, FacilityId = 43, SuggestionId = 103, },
                  new SuggestionFacility { Id = 200, FacilityId = 44, SuggestionId = 103, },
                  new SuggestionFacility { Id = 201, FacilityId = 45, SuggestionId = 103, },
                  new SuggestionFacility { Id = 202, FacilityId = 46, SuggestionId = 103, },
                  new SuggestionFacility { Id = 203, FacilityId = 47, SuggestionId = 103, },
                  new SuggestionFacility { Id = 204, FacilityId = 1, SuggestionId = 103, },
                  new SuggestionFacility { Id = 205, FacilityId = 2, SuggestionId = 103, },
                  new SuggestionFacility { Id = 206, FacilityId = 3, SuggestionId = 103, },
                  new SuggestionFacility { Id = 207, FacilityId = 4, SuggestionId = 103, },
                  new SuggestionFacility { Id = 208, FacilityId = 5, SuggestionId = 103, },
                  new SuggestionFacility { Id = 209, FacilityId = 6, SuggestionId = 103, },
                  new SuggestionFacility { Id = 210, FacilityId = 7, SuggestionId = 103, },

                  new SuggestionFacility { Id = 211, FacilityId = 20, SuggestionId = 97, },
                  new SuggestionFacility { Id = 212, FacilityId = 30, SuggestionId = 97, },
                  new SuggestionFacility { Id = 213, FacilityId = 31, SuggestionId = 97, },
                  new SuggestionFacility { Id = 214, FacilityId = 32, SuggestionId = 97, },
                  new SuggestionFacility { Id = 215, FacilityId = 33, SuggestionId = 97, },
                  new SuggestionFacility { Id = 216, FacilityId = 34, SuggestionId = 97, },
                  new SuggestionFacility { Id = 217, FacilityId = 35, SuggestionId = 97, },
                  new SuggestionFacility { Id = 218, FacilityId = 36, SuggestionId = 97, },
                  new SuggestionFacility { Id = 219, FacilityId = 37, SuggestionId = 97, },
                  new SuggestionFacility { Id = 220, FacilityId = 38, SuggestionId = 97, },
                  new SuggestionFacility { Id = 221, FacilityId = 39, SuggestionId = 97, },
                  new SuggestionFacility { Id = 222, FacilityId = 40, SuggestionId = 97, },
                  new SuggestionFacility { Id = 223, FacilityId = 41, SuggestionId = 97, },
                  new SuggestionFacility { Id = 224, FacilityId = 42, SuggestionId = 97, },
                  new SuggestionFacility { Id = 225, FacilityId = 43, SuggestionId = 97, },
                  new SuggestionFacility { Id = 226, FacilityId = 44, SuggestionId = 97, },
                  new SuggestionFacility { Id = 227, FacilityId = 45, SuggestionId = 97, },
                  new SuggestionFacility { Id = 228, FacilityId = 46, SuggestionId = 97, },
                  new SuggestionFacility { Id = 229, FacilityId = 47, SuggestionId = 97, },
                  new SuggestionFacility { Id = 230, FacilityId = 1, SuggestionId = 97, },
                  new SuggestionFacility { Id = 231, FacilityId = 2, SuggestionId = 97, },
                  new SuggestionFacility { Id = 232, FacilityId = 3, SuggestionId = 97, },
                  new SuggestionFacility { Id = 233, FacilityId = 4, SuggestionId = 97, },
                  new SuggestionFacility { Id = 234, FacilityId = 5, SuggestionId = 97, },
                  new SuggestionFacility { Id = 235, FacilityId = 6, SuggestionId = 97, },
                  new SuggestionFacility { Id = 236, FacilityId = 7, SuggestionId = 97, },

                  new SuggestionFacility { Id = 237, FacilityId = 20, SuggestionId = 7, },
                  new SuggestionFacility { Id = 238, FacilityId = 30, SuggestionId = 7, },
                  new SuggestionFacility { Id = 239, FacilityId = 31, SuggestionId = 7, },
                  new SuggestionFacility { Id = 240, FacilityId = 32, SuggestionId = 7, },
                  new SuggestionFacility { Id = 241, FacilityId = 33, SuggestionId = 7, },
                  new SuggestionFacility { Id = 242, FacilityId = 34, SuggestionId = 7, },
                  new SuggestionFacility { Id = 243, FacilityId = 35, SuggestionId = 7, },
                  new SuggestionFacility { Id = 244, FacilityId = 36, SuggestionId = 7, },
                  new SuggestionFacility { Id = 245, FacilityId = 37, SuggestionId = 7, },
                  new SuggestionFacility { Id = 246, FacilityId = 38, SuggestionId = 7, },
                  new SuggestionFacility { Id = 247, FacilityId = 39, SuggestionId = 7, },
                  new SuggestionFacility { Id = 248, FacilityId = 40, SuggestionId = 7, },
                  new SuggestionFacility { Id = 249, FacilityId = 41, SuggestionId = 7, },
                  new SuggestionFacility { Id = 250, FacilityId = 42, SuggestionId = 7, },
                  new SuggestionFacility { Id = 251, FacilityId = 43, SuggestionId = 7, },
                  new SuggestionFacility { Id = 252, FacilityId = 44, SuggestionId = 7, },
                  new SuggestionFacility { Id = 253, FacilityId = 45, SuggestionId = 7, },
                  new SuggestionFacility { Id = 254, FacilityId = 46, SuggestionId = 7, },
                  new SuggestionFacility { Id = 255, FacilityId = 47, SuggestionId = 7, },
                  new SuggestionFacility { Id = 256, FacilityId = 1, SuggestionId = 7, },
                  new SuggestionFacility { Id = 257, FacilityId = 2, SuggestionId = 7, },
                  new SuggestionFacility { Id = 258, FacilityId = 3, SuggestionId = 7, },
                  new SuggestionFacility { Id = 259, FacilityId = 4, SuggestionId = 7, },
                  new SuggestionFacility { Id = 260, FacilityId = 5, SuggestionId = 7, },
                  new SuggestionFacility { Id = 261, FacilityId = 6, SuggestionId = 7, },
                  new SuggestionFacility { Id = 262, FacilityId = 7, SuggestionId = 7, },

                  new SuggestionFacility { Id = 263, FacilityId = 20, SuggestionId = 11, },
                  new SuggestionFacility { Id = 264, FacilityId = 30, SuggestionId = 11, },
                  new SuggestionFacility { Id = 265, FacilityId = 31, SuggestionId = 11, },
                  new SuggestionFacility { Id = 266, FacilityId = 32, SuggestionId = 11, },
                  new SuggestionFacility { Id = 267, FacilityId = 33, SuggestionId = 11, },
                  new SuggestionFacility { Id = 268, FacilityId = 34, SuggestionId = 11, },
                  new SuggestionFacility { Id = 269, FacilityId = 35, SuggestionId = 11, },
                  new SuggestionFacility { Id = 270, FacilityId = 36, SuggestionId = 11, },
                  new SuggestionFacility { Id = 271, FacilityId = 37, SuggestionId = 11, },
                  new SuggestionFacility { Id = 272, FacilityId = 38, SuggestionId = 11, },
                  new SuggestionFacility { Id = 273, FacilityId = 39, SuggestionId = 11, },
                  new SuggestionFacility { Id = 274, FacilityId = 40, SuggestionId = 11, },
                  new SuggestionFacility { Id = 275, FacilityId = 41, SuggestionId = 11, },
                  new SuggestionFacility { Id = 276, FacilityId = 42, SuggestionId = 11, },
                  new SuggestionFacility { Id = 277, FacilityId = 43, SuggestionId = 11, },
                  new SuggestionFacility { Id = 278, FacilityId = 44, SuggestionId = 11, },
                  new SuggestionFacility { Id = 279, FacilityId = 45, SuggestionId = 11, },
                  new SuggestionFacility { Id = 280, FacilityId = 46, SuggestionId = 11, },
                  new SuggestionFacility { Id = 281, FacilityId = 47, SuggestionId = 11, },
                  new SuggestionFacility { Id = 282, FacilityId = 1, SuggestionId = 11, },
                  new SuggestionFacility { Id = 283, FacilityId = 2, SuggestionId = 11, },
                  new SuggestionFacility { Id = 284, FacilityId = 3, SuggestionId = 11, },
                  new SuggestionFacility { Id = 285, FacilityId = 4, SuggestionId = 11, },
                  new SuggestionFacility { Id = 286, FacilityId = 5, SuggestionId = 11, },
                  new SuggestionFacility { Id = 287, FacilityId = 6, SuggestionId = 11, },
                  new SuggestionFacility { Id = 288, FacilityId = 7, SuggestionId = 11, },

                  new SuggestionFacility { Id = 289, FacilityId = 20, SuggestionId = 33, },
                  new SuggestionFacility { Id = 290, FacilityId = 30, SuggestionId = 33, },
                  new SuggestionFacility { Id = 291, FacilityId = 31, SuggestionId = 33, },
                  new SuggestionFacility { Id = 292, FacilityId = 32, SuggestionId = 33, },
                  new SuggestionFacility { Id = 293, FacilityId = 33, SuggestionId = 33, },
                  new SuggestionFacility { Id = 294, FacilityId = 34, SuggestionId = 33, },
                  new SuggestionFacility { Id = 295, FacilityId = 35, SuggestionId = 33, },
                  new SuggestionFacility { Id = 296, FacilityId = 36, SuggestionId = 33, },
                  new SuggestionFacility { Id = 297, FacilityId = 37, SuggestionId = 33, },
                  new SuggestionFacility { Id = 298, FacilityId = 38, SuggestionId = 33, },
                  new SuggestionFacility { Id = 299, FacilityId = 39, SuggestionId = 33, },
                  new SuggestionFacility { Id = 300, FacilityId = 40, SuggestionId = 33, },
                  new SuggestionFacility { Id = 301, FacilityId = 41, SuggestionId = 33, },
                  new SuggestionFacility { Id = 302, FacilityId = 42, SuggestionId = 33, },
                  new SuggestionFacility { Id = 303, FacilityId = 43, SuggestionId = 33, },
                  new SuggestionFacility { Id = 304, FacilityId = 44, SuggestionId = 33, },
                  new SuggestionFacility { Id = 305, FacilityId = 45, SuggestionId = 33, },
                  new SuggestionFacility { Id = 306, FacilityId = 46, SuggestionId = 33, },
                  new SuggestionFacility { Id = 307, FacilityId = 47, SuggestionId = 33, },
                  new SuggestionFacility { Id = 308, FacilityId = 1, SuggestionId = 33, },
                  new SuggestionFacility { Id = 309, FacilityId = 2, SuggestionId = 33, },
                  new SuggestionFacility { Id = 310, FacilityId = 3, SuggestionId = 33, },
                  new SuggestionFacility { Id = 311, FacilityId = 4, SuggestionId = 33, },
                  new SuggestionFacility { Id = 312, FacilityId = 5, SuggestionId = 33, },
                  new SuggestionFacility { Id = 313, FacilityId = 6, SuggestionId = 33, },
                  new SuggestionFacility { Id = 314, FacilityId = 7, SuggestionId = 33, },
              });
        }
    }
}
