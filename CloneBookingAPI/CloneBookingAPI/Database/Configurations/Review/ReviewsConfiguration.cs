﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloneBookingAPI.Services.Database.Configurations.Review
{
    public class ReviewsConfiguration : IEntityTypeConfiguration<Models.Review.Review>
    {
        public void Configure(EntityTypeBuilder<Models.Review.Review> builder)
        {
            builder.HasData(
              new Models.Review.Review[]
              {
                  new Models.Review.Review { Id = 1, SuggestionId = 15, UserId = 1, StayBookingId = 1, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 2, SuggestionId = 15, UserId = 2, StayBookingId = 1, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 3, SuggestionId = 15, UserId = 3, StayBookingId = 1, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 4, SuggestionId = 15, UserId = 4, StayBookingId = 1, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 5, SuggestionId = 15, UserId = 1, StayBookingId = 1, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 6, SuggestionId = 1, UserId = 2, StayBookingId = 1, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 7, SuggestionId = 1, UserId = 3, StayBookingId = 1, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 8, SuggestionId = 1, UserId = 4, StayBookingId = 1, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 9, SuggestionId = 1, UserId = 1, StayBookingId = 1, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 10, SuggestionId = 1, UserId = 2, StayBookingId = 1, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 11, SuggestionId = 7, UserId = 3, StayBookingId = 2, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 12, SuggestionId = 7, UserId = 4, StayBookingId = 2, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 13, SuggestionId = 7, UserId = 1, StayBookingId = 2, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 14, SuggestionId = 7, UserId = 2, StayBookingId = 2, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 15, SuggestionId = 7, UserId = 3, StayBookingId = 2, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 16, SuggestionId = 11, UserId = 4, StayBookingId = 2, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 17, SuggestionId = 11, UserId = 1, StayBookingId = 2, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 18, SuggestionId = 11, UserId = 2, StayBookingId = 2, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 19, SuggestionId = 11, UserId = 3, StayBookingId = 2, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 20, SuggestionId = 11, UserId = 4, StayBookingId = 2, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 21, SuggestionId = 33, UserId = 1, StayBookingId = 3, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 22, SuggestionId = 33, UserId = 2, StayBookingId = 3, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 23, SuggestionId = 33, UserId = 3, StayBookingId = 3, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 24, SuggestionId = 33, UserId = 4, StayBookingId = 3, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 25, SuggestionId = 33, UserId = 1, StayBookingId = 3, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 26, SuggestionId = 15, UserId = 2, StayBookingId = 3, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 27, SuggestionId = 15, UserId = 3, StayBookingId = 3, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 28, SuggestionId = 15, UserId = 4, StayBookingId = 3, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 29, SuggestionId = 15, UserId = 1, StayBookingId = 3, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 30, SuggestionId = 15, UserId = 2, StayBookingId = 3, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 31, SuggestionId = 27, UserId = 3, StayBookingId = 4, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 32, SuggestionId = 27, UserId = 4, StayBookingId = 4, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 33, SuggestionId = 27, UserId = 1, StayBookingId = 4, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 34, SuggestionId = 27, UserId = 2, StayBookingId = 4, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 35, SuggestionId = 27, UserId = 3, StayBookingId = 4, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 36, SuggestionId = 57, UserId = 4, StayBookingId = 4, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 37, SuggestionId = 57, UserId = 1, StayBookingId = 4, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 38, SuggestionId = 57, UserId = 2, StayBookingId = 4, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 39, SuggestionId = 57, UserId = 3, StayBookingId = 4, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 40, SuggestionId = 57, UserId = 4, StayBookingId = 4, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 41, SuggestionId = 101, UserId = 1, StayBookingId = 5, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 42, SuggestionId = 101, UserId = 2, StayBookingId = 5, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 43, SuggestionId = 101, UserId = 3, StayBookingId = 5, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 44, SuggestionId = 101, UserId = 4, StayBookingId = 5, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 45, SuggestionId = 101, UserId = 1, StayBookingId = 5, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 46, SuggestionId = 28, UserId = 2, StayBookingId = 5, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 47, SuggestionId = 28, UserId = 3, StayBookingId = 5, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 48, SuggestionId = 28, UserId = 4, StayBookingId = 5, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 49, SuggestionId = 28, UserId = 1, StayBookingId = 5, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 50, SuggestionId = 28, UserId = 2, StayBookingId = 5, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 51, SuggestionId = 19, UserId = 3, StayBookingId = 6, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 52, SuggestionId = 19, UserId = 4, StayBookingId = 6, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 53, SuggestionId = 19, UserId = 1, StayBookingId = 6, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 54, SuggestionId = 19, UserId = 2, StayBookingId = 6, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 55, SuggestionId = 19, UserId = 3, StayBookingId = 6, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 56, SuggestionId = 103, UserId = 4, StayBookingId = 6, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 57, SuggestionId = 103, UserId = 1, StayBookingId = 6, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 58, SuggestionId = 103, UserId = 2, StayBookingId = 6, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 59, SuggestionId = 103, UserId = 3, StayBookingId = 6, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 60, SuggestionId = 103, UserId = 4, StayBookingId = 6, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 61, SuggestionId = 97, UserId = 1, StayBookingId = 7, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 62, SuggestionId = 97, UserId = 2, StayBookingId = 7, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 63, SuggestionId = 97, UserId = 3, StayBookingId = 7, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 64, SuggestionId = 97, UserId = 4, StayBookingId = 7, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 65, SuggestionId = 97, UserId = 1, StayBookingId = 7, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 66, SuggestionId = 12, UserId = 2, StayBookingId = 7, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 67, SuggestionId = 13, UserId = 3, StayBookingId = 7, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 68, SuggestionId = 2, UserId = 4, StayBookingId = 7, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 69, SuggestionId = 3, UserId = 1, StayBookingId = 7, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 70, SuggestionId = 4, UserId = 2, StayBookingId = 7, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 71, SuggestionId = 5, UserId = 3, StayBookingId = 8, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 72, SuggestionId = 6, UserId = 4, StayBookingId = 8, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 73, SuggestionId = 9, UserId = 1, StayBookingId = 8, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 74, SuggestionId = 8, UserId = 2, StayBookingId = 8, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 75, SuggestionId = 38, UserId = 3, StayBookingId = 8, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 76, SuggestionId = 24, UserId = 4, StayBookingId = 8, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 77, SuggestionId = 24, UserId = 1, StayBookingId = 8, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 78, SuggestionId = 41, UserId = 2, StayBookingId = 8, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 79, SuggestionId = 42, UserId = 3, StayBookingId = 8, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 80, SuggestionId = 43, UserId = 4, StayBookingId = 8, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 81, SuggestionId = 45, UserId = 1, StayBookingId = 9, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 82, SuggestionId = 56, UserId = 2, StayBookingId = 9, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 83, SuggestionId = 56, UserId = 3, StayBookingId = 9, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 84, SuggestionId = 96, UserId = 4, StayBookingId = 9, ReviewedDate = DateTime.UtcNow, },
                  new Models.Review.Review { Id = 85, SuggestionId = 94, UserId = 1, StayBookingId = 9, ReviewedDate = DateTime.UtcNow, },
              });
        }
    }
}
