﻿using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class SuggestionsFileModelsConfiguration : IEntityTypeConfiguration<SuggestionFileModel>
    {
        public void Configure(EntityTypeBuilder<SuggestionFileModel> builder)
        {
            builder.HasData(
              new SuggestionFileModel[]
              {
                  new SuggestionFileModel { Id = 1, ImageId = 24, SuggestionId = 1, },
                  new SuggestionFileModel { Id = 2, ImageId = 25, SuggestionId = 2, },
                  new SuggestionFileModel { Id = 3, ImageId = 26, SuggestionId = 3, },
                  new SuggestionFileModel { Id = 4, ImageId = 27, SuggestionId = 4, },
                  new SuggestionFileModel { Id = 5, ImageId = 28, SuggestionId = 5, },
                  new SuggestionFileModel { Id = 6, ImageId = 24, SuggestionId = 6, },
                  new SuggestionFileModel { Id = 7, ImageId = 25, SuggestionId = 7, },
                  new SuggestionFileModel { Id = 8, ImageId = 26, SuggestionId = 8, },
                  new SuggestionFileModel { Id = 9, ImageId = 27, SuggestionId = 9, },
                  new SuggestionFileModel { Id = 10, ImageId = 28, SuggestionId = 10, },
                  new SuggestionFileModel { Id = 11, ImageId = 24, SuggestionId = 11, },
                  new SuggestionFileModel { Id = 12, ImageId = 25, SuggestionId = 12, },
                  new SuggestionFileModel { Id = 13, ImageId = 26, SuggestionId = 13, },
                  new SuggestionFileModel { Id = 14, ImageId = 27, SuggestionId = 14, },
                  new SuggestionFileModel { Id = 15, ImageId = 28, SuggestionId = 15, },
                  new SuggestionFileModel { Id = 16, ImageId = 24, SuggestionId = 16, },
                  new SuggestionFileModel { Id = 17, ImageId = 25, SuggestionId = 17, },
                  new SuggestionFileModel { Id = 18, ImageId = 26, SuggestionId = 18, },
                  new SuggestionFileModel { Id = 19, ImageId = 27, SuggestionId = 19, },
                  new SuggestionFileModel { Id = 20, ImageId = 28, SuggestionId = 20, },
                  new SuggestionFileModel { Id = 21, ImageId = 24, SuggestionId = 21, },
                  new SuggestionFileModel { Id = 22, ImageId = 25, SuggestionId = 22, },
                  new SuggestionFileModel { Id = 23, ImageId = 26, SuggestionId = 23, },
                  new SuggestionFileModel { Id = 24, ImageId = 27, SuggestionId = 24, },
                  new SuggestionFileModel { Id = 25, ImageId = 28, SuggestionId = 25, },
                  new SuggestionFileModel { Id = 26, ImageId = 24, SuggestionId = 26, },
                  new SuggestionFileModel { Id = 27, ImageId = 25, SuggestionId = 27, },
                  new SuggestionFileModel { Id = 28, ImageId = 26, SuggestionId = 28, },
                  new SuggestionFileModel { Id = 29, ImageId = 27, SuggestionId = 29, },
                  new SuggestionFileModel { Id = 30, ImageId = 28, SuggestionId = 30, },
                  new SuggestionFileModel { Id = 31, ImageId = 24, SuggestionId = 31, },
                  new SuggestionFileModel { Id = 32, ImageId = 25, SuggestionId = 32, },
                  new SuggestionFileModel { Id = 33, ImageId = 26, SuggestionId = 33, },
                  new SuggestionFileModel { Id = 34, ImageId = 27, SuggestionId = 34, },
                  new SuggestionFileModel { Id = 35, ImageId = 28, SuggestionId = 35, },
                  new SuggestionFileModel { Id = 36, ImageId = 24, SuggestionId = 36, },
                  new SuggestionFileModel { Id = 37, ImageId = 25, SuggestionId = 37, },
                  new SuggestionFileModel { Id = 38, ImageId = 26, SuggestionId = 38, },
                  new SuggestionFileModel { Id = 39, ImageId = 27, SuggestionId = 39, },
                  new SuggestionFileModel { Id = 40, ImageId = 28, SuggestionId = 40, },
                  new SuggestionFileModel { Id = 41, ImageId = 24, SuggestionId = 41, },
                  new SuggestionFileModel { Id = 42, ImageId = 25, SuggestionId = 42, },
                  new SuggestionFileModel { Id = 43, ImageId = 26, SuggestionId = 43, },
                  new SuggestionFileModel { Id = 44, ImageId = 27, SuggestionId = 44, },
                  new SuggestionFileModel { Id = 45, ImageId = 28, SuggestionId = 45, },
                  new SuggestionFileModel { Id = 46, ImageId = 24, SuggestionId = 46, },
                  new SuggestionFileModel { Id = 47, ImageId = 25, SuggestionId = 47, },
                  new SuggestionFileModel { Id = 48, ImageId = 26, SuggestionId = 48, },
                  new SuggestionFileModel { Id = 49, ImageId = 27, SuggestionId = 49, },
                  new SuggestionFileModel { Id = 50, ImageId = 28, SuggestionId = 50, },
                  new SuggestionFileModel { Id = 51, ImageId = 24, SuggestionId = 51, },
                  new SuggestionFileModel { Id = 52, ImageId = 25, SuggestionId = 52, },
                  new SuggestionFileModel { Id = 53, ImageId = 26, SuggestionId = 53, },
                  new SuggestionFileModel { Id = 54, ImageId = 27, SuggestionId = 54, },
                  new SuggestionFileModel { Id = 55, ImageId = 28, SuggestionId = 55, },
                  new SuggestionFileModel { Id = 56, ImageId = 24, SuggestionId = 56, },
                  new SuggestionFileModel { Id = 57, ImageId = 25, SuggestionId = 57, },
                  new SuggestionFileModel { Id = 58, ImageId = 26, SuggestionId = 58, },
                  new SuggestionFileModel { Id = 59, ImageId = 27, SuggestionId = 59, },
                  new SuggestionFileModel { Id = 60, ImageId = 28, SuggestionId = 60, },
                  new SuggestionFileModel { Id = 61, ImageId = 24, SuggestionId = 61, },
                  new SuggestionFileModel { Id = 62, ImageId = 25, SuggestionId = 62, },
                  new SuggestionFileModel { Id = 63, ImageId = 26, SuggestionId = 63, },
                  new SuggestionFileModel { Id = 64, ImageId = 27, SuggestionId = 64, },
                  new SuggestionFileModel { Id = 65, ImageId = 28, SuggestionId = 65, },
                  new SuggestionFileModel { Id = 66, ImageId = 24, SuggestionId = 66, },
                  new SuggestionFileModel { Id = 67, ImageId = 25, SuggestionId = 67, },
                  new SuggestionFileModel { Id = 68, ImageId = 26, SuggestionId = 68, },
                  new SuggestionFileModel { Id = 69, ImageId = 27, SuggestionId = 69, },
                  new SuggestionFileModel { Id = 70, ImageId = 28, SuggestionId = 70, },
                  new SuggestionFileModel { Id = 71, ImageId = 24, SuggestionId = 71, },
                  new SuggestionFileModel { Id = 72, ImageId = 25, SuggestionId = 72, },
                  new SuggestionFileModel { Id = 73, ImageId = 26, SuggestionId = 73, },
                  new SuggestionFileModel { Id = 74, ImageId = 27, SuggestionId = 74, },
                  new SuggestionFileModel { Id = 75, ImageId = 28, SuggestionId = 75, },
                  new SuggestionFileModel { Id = 76, ImageId = 24, SuggestionId = 76, },
                  new SuggestionFileModel { Id = 77, ImageId = 25, SuggestionId = 77, },
                  new SuggestionFileModel { Id = 78, ImageId = 26, SuggestionId = 78, },
                  new SuggestionFileModel { Id = 79, ImageId = 27, SuggestionId = 79, },
                  new SuggestionFileModel { Id = 80, ImageId = 28, SuggestionId = 80, },
                  new SuggestionFileModel { Id = 81, ImageId = 24, SuggestionId = 81, },
                  new SuggestionFileModel { Id = 82, ImageId = 25, SuggestionId = 82, },
                  new SuggestionFileModel { Id = 83, ImageId = 26, SuggestionId = 83, },
                  new SuggestionFileModel { Id = 84, ImageId = 27, SuggestionId = 84, },
                  new SuggestionFileModel { Id = 85, ImageId = 28, SuggestionId = 85, },
                  new SuggestionFileModel { Id = 86, ImageId = 24, SuggestionId = 86, },
                  new SuggestionFileModel { Id = 87, ImageId = 25, SuggestionId = 87, },
                  new SuggestionFileModel { Id = 88, ImageId = 26, SuggestionId = 88, },
                  new SuggestionFileModel { Id = 89, ImageId = 27, SuggestionId = 89, },
                  new SuggestionFileModel { Id = 90, ImageId = 28, SuggestionId = 90, },
                  new SuggestionFileModel { Id = 91, ImageId = 24, SuggestionId = 91, },
                  new SuggestionFileModel { Id = 92, ImageId = 25, SuggestionId = 92, },
                  new SuggestionFileModel { Id = 93, ImageId = 26, SuggestionId = 93, },
                  new SuggestionFileModel { Id = 94, ImageId = 27, SuggestionId = 94, },
                  new SuggestionFileModel { Id = 95, ImageId = 28, SuggestionId = 95, },
                  new SuggestionFileModel { Id = 96, ImageId = 24, SuggestionId = 96, },
                  new SuggestionFileModel { Id = 97, ImageId = 25, SuggestionId = 97, },
                  new SuggestionFileModel { Id = 98, ImageId = 26, SuggestionId = 98, },
                  new SuggestionFileModel { Id = 99, ImageId = 27, SuggestionId = 99, },
                  new SuggestionFileModel { Id = 100, ImageId = 28, SuggestionId = 100, },
                  new SuggestionFileModel { Id = 101, ImageId = 24, SuggestionId = 101, },
                  new SuggestionFileModel { Id = 102, ImageId = 25, SuggestionId = 102, },
                  new SuggestionFileModel { Id = 103, ImageId = 26, SuggestionId = 103, },
                  new SuggestionFileModel { Id = 104, ImageId = 27, SuggestionId = 104, },
                  new SuggestionFileModel { Id = 105, ImageId = 28, SuggestionId = 105, },
                  new SuggestionFileModel { Id = 106, ImageId = 24, SuggestionId = 106, },
                  new SuggestionFileModel { Id = 107, ImageId = 25, SuggestionId = 107, },
                  new SuggestionFileModel { Id = 108, ImageId = 26, SuggestionId = 108, },
                  new SuggestionFileModel { Id = 109, ImageId = 27, SuggestionId = 109, },
                  new SuggestionFileModel { Id = 110, ImageId = 28, SuggestionId = 110, },
                  new SuggestionFileModel { Id = 111, ImageId = 24, SuggestionId = 111, },
                  new SuggestionFileModel { Id = 112, ImageId = 25, SuggestionId = 112, },
                  new SuggestionFileModel { Id = 113, ImageId = 26, SuggestionId = 113, },
                  new SuggestionFileModel { Id = 114, ImageId = 27, SuggestionId = 114, },
                  new SuggestionFileModel { Id = 115, ImageId = 28, SuggestionId = 115, },
                  new SuggestionFileModel { Id = 116, ImageId = 24, SuggestionId = 116, },
                  new SuggestionFileModel { Id = 117, ImageId = 25, SuggestionId = 117, },
                  new SuggestionFileModel { Id = 118, ImageId = 26, SuggestionId = 118, },
                  new SuggestionFileModel { Id = 119, ImageId = 27, SuggestionId = 119, },
                  new SuggestionFileModel { Id = 120, ImageId = 28, SuggestionId = 120, },
              });
        }
    }
}
