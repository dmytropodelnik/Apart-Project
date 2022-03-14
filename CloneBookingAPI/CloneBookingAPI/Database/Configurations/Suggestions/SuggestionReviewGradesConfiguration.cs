using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Suggestions
{
    public class SuggestionReviewGradesConfiguration : IEntityTypeConfiguration<SuggestionReviewGrade>
    {
        public void Configure(EntityTypeBuilder<SuggestionReviewGrade> builder)
        {
            builder.HasData(
              new SuggestionReviewGrade[]
              {
                  new SuggestionReviewGrade { Id = 1, Value = 10, ReviewCategoryId = 1, SuggestionId = 1, },
                  new SuggestionReviewGrade { Id = 2, Value = 7, ReviewCategoryId = 2, SuggestionId = 2, },
                  new SuggestionReviewGrade { Id = 3, Value = 6, ReviewCategoryId = 3, SuggestionId = 3, },
                  new SuggestionReviewGrade { Id = 4, Value = 9, ReviewCategoryId = 4, SuggestionId = 4, },
                  new SuggestionReviewGrade { Id = 5, Value = 6, ReviewCategoryId = 5, SuggestionId = 5, },
                  new SuggestionReviewGrade { Id = 6, Value = 9, ReviewCategoryId = 6, SuggestionId = 1, },
                  new SuggestionReviewGrade { Id = 7, Value = 6, ReviewCategoryId = 7, SuggestionId = 2, },
                  new SuggestionReviewGrade { Id = 8, Value = 3, ReviewCategoryId = 8, SuggestionId = 3, },
                  new SuggestionReviewGrade { Id = 9, Value = 8, ReviewCategoryId = 9, SuggestionId = 4, },
                  new SuggestionReviewGrade { Id = 10, Value = 5, ReviewCategoryId = 10, SuggestionId = 5, },
                  new SuggestionReviewGrade { Id = 11, Value = 10, ReviewCategoryId = 11, SuggestionId = 1, },
                  new SuggestionReviewGrade { Id = 12, Value = 5, ReviewCategoryId = 12, SuggestionId = 2, },
                  new SuggestionReviewGrade { Id = 13, Value = 9, ReviewCategoryId = 1, SuggestionId = 3, },
                  new SuggestionReviewGrade { Id = 14, Value = 7, ReviewCategoryId = 2, SuggestionId = 4, },
                  new SuggestionReviewGrade { Id = 15, Value = 6, ReviewCategoryId = 3, SuggestionId = 5, },
                  new SuggestionReviewGrade { Id = 16, Value = 9, ReviewCategoryId = 4, SuggestionId = 1, },
                  new SuggestionReviewGrade { Id = 17, Value = 6, ReviewCategoryId = 5, SuggestionId = 2, },
                  new SuggestionReviewGrade { Id = 18, Value = 4, ReviewCategoryId = 6, SuggestionId = 3, },
                  new SuggestionReviewGrade { Id = 19, Value = 6, ReviewCategoryId = 7, SuggestionId = 4, },
                  new SuggestionReviewGrade { Id = 20, Value = 3, ReviewCategoryId = 8, SuggestionId = 5, },
                  new SuggestionReviewGrade { Id = 21, Value = 9, ReviewCategoryId = 9, SuggestionId = 1, },
                  new SuggestionReviewGrade { Id = 22, Value = 5, ReviewCategoryId = 10, SuggestionId = 2, },
                  new SuggestionReviewGrade { Id = 23, Value = 7, ReviewCategoryId = 11, SuggestionId = 3, },
                  new SuggestionReviewGrade { Id = 24, Value = 5, ReviewCategoryId = 12, SuggestionId = 4, },
                  new SuggestionReviewGrade { Id = 25, Value = 9, ReviewCategoryId = 1, SuggestionId = 5, },
                  new SuggestionReviewGrade { Id = 26, Value = 10, ReviewCategoryId = 2, SuggestionId = 1, },
                  new SuggestionReviewGrade { Id = 27, Value = 6, ReviewCategoryId = 3, SuggestionId = 2, },
                  new SuggestionReviewGrade { Id = 28, Value = 9, ReviewCategoryId = 4, SuggestionId = 3, },
                  new SuggestionReviewGrade { Id = 29, Value = 6, ReviewCategoryId = 5, SuggestionId = 4, },
                  new SuggestionReviewGrade { Id = 30, Value = 4, ReviewCategoryId = 6, SuggestionId = 5, },
                  new SuggestionReviewGrade { Id = 31, Value = 9, ReviewCategoryId = 7, SuggestionId = 1, },
                  new SuggestionReviewGrade { Id = 32, Value = 3, ReviewCategoryId = 8, SuggestionId = 2, },
                  new SuggestionReviewGrade { Id = 33, Value = 8, ReviewCategoryId = 9, SuggestionId = 3, },
                  new SuggestionReviewGrade { Id = 34, Value = 5, ReviewCategoryId = 10, SuggestionId = 4, },
                  new SuggestionReviewGrade { Id = 35, Value = 7, ReviewCategoryId = 11, SuggestionId = 5, },
                  new SuggestionReviewGrade { Id = 36, Value = 9, ReviewCategoryId = 12, SuggestionId = 1, },
              });
        }
    }
}
