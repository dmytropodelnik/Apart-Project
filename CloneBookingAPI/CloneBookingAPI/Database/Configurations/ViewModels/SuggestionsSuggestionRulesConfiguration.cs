using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class SuggestionsSuggestionRulesConfiguration : IEntityTypeConfiguration<SuggestionSuggestionRule>
    {
        public void Configure(EntityTypeBuilder<SuggestionSuggestionRule> builder)
        {
            builder.HasData(
              new SuggestionSuggestionRule[]
              {
                    new SuggestionSuggestionRule { Id = 1, SuggestionId = 1, SuggestionRuleId = 1, },
                    new SuggestionSuggestionRule { Id = 2, SuggestionId = 1, SuggestionRuleId = 2, },
                    new SuggestionSuggestionRule { Id = 3, SuggestionId = 1, SuggestionRuleId = 3, },
                    new SuggestionSuggestionRule { Id = 4, SuggestionId = 1, SuggestionRuleId = 4, },
                    new SuggestionSuggestionRule { Id = 5, SuggestionId = 1, SuggestionRuleId = 5, },
                    new SuggestionSuggestionRule { Id = 6, SuggestionId = 1, SuggestionRuleId = 6, },
                    new SuggestionSuggestionRule { Id = 7, SuggestionId = 1, SuggestionRuleId = 7, },
                    new SuggestionSuggestionRule { Id = 8, SuggestionId = 1, SuggestionRuleId = 8, },
                    new SuggestionSuggestionRule { Id = 9, SuggestionId = 1, SuggestionRuleId = 9, },
                    new SuggestionSuggestionRule { Id = 10, SuggestionId = 1, SuggestionRuleId = 10, },
                    new SuggestionSuggestionRule { Id = 11, SuggestionId = 1, SuggestionRuleId = 11, },

                    new SuggestionSuggestionRule { Id = 12, SuggestionId = 15, SuggestionRuleId = 1, },
                    new SuggestionSuggestionRule { Id = 13, SuggestionId = 15, SuggestionRuleId = 2, },
                    new SuggestionSuggestionRule { Id = 14, SuggestionId = 15, SuggestionRuleId = 3, },
                    new SuggestionSuggestionRule { Id = 15, SuggestionId = 15, SuggestionRuleId = 4, },
                    new SuggestionSuggestionRule { Id = 16, SuggestionId = 15, SuggestionRuleId = 5, },
                    new SuggestionSuggestionRule { Id = 17, SuggestionId = 15, SuggestionRuleId = 6, },
                    new SuggestionSuggestionRule { Id = 18, SuggestionId = 15, SuggestionRuleId = 7, },

                    new SuggestionSuggestionRule { Id = 19, SuggestionId = 27, SuggestionRuleId = 6, },
                    new SuggestionSuggestionRule { Id = 20, SuggestionId = 27, SuggestionRuleId = 7, },
                    new SuggestionSuggestionRule { Id = 21, SuggestionId = 27, SuggestionRuleId = 8, },
                    new SuggestionSuggestionRule { Id = 22, SuggestionId = 27, SuggestionRuleId = 9, },
                    new SuggestionSuggestionRule { Id = 23, SuggestionId = 27, SuggestionRuleId = 10, },
                    new SuggestionSuggestionRule { Id = 24, SuggestionId = 27, SuggestionRuleId = 11, },

                    new SuggestionSuggestionRule { Id = 25, SuggestionId = 57, SuggestionRuleId = 1, },
                    new SuggestionSuggestionRule { Id = 26, SuggestionId = 57, SuggestionRuleId = 2, },
                    new SuggestionSuggestionRule { Id = 27, SuggestionId = 57, SuggestionRuleId = 3, },
                    new SuggestionSuggestionRule { Id = 28, SuggestionId = 57, SuggestionRuleId = 7, },
                    new SuggestionSuggestionRule { Id = 29, SuggestionId = 57, SuggestionRuleId = 8, },
                    new SuggestionSuggestionRule { Id = 30, SuggestionId = 57, SuggestionRuleId = 9, },
                    new SuggestionSuggestionRule { Id = 31, SuggestionId = 57, SuggestionRuleId = 10, },
                    new SuggestionSuggestionRule { Id = 32, SuggestionId = 57, SuggestionRuleId = 11, },

                    new SuggestionSuggestionRule { Id = 33, SuggestionId = 101, SuggestionRuleId = 1, },
                    new SuggestionSuggestionRule { Id = 34, SuggestionId = 101, SuggestionRuleId = 2, },
                    new SuggestionSuggestionRule { Id = 35, SuggestionId = 101, SuggestionRuleId = 3, },
                    new SuggestionSuggestionRule { Id = 36, SuggestionId = 101, SuggestionRuleId = 4, },
                    new SuggestionSuggestionRule { Id = 37, SuggestionId = 101, SuggestionRuleId = 8, },
                    new SuggestionSuggestionRule { Id = 38, SuggestionId = 101, SuggestionRuleId = 9, },
                    new SuggestionSuggestionRule { Id = 39, SuggestionId = 101, SuggestionRuleId = 10, },
                    new SuggestionSuggestionRule { Id = 40, SuggestionId = 101, SuggestionRuleId = 11, },

                    new SuggestionSuggestionRule { Id = 41, SuggestionId = 28, SuggestionRuleId = 1, },
                    new SuggestionSuggestionRule { Id = 42, SuggestionId = 28, SuggestionRuleId = 5, },
                    new SuggestionSuggestionRule { Id = 43, SuggestionId = 28, SuggestionRuleId = 6, },
                    new SuggestionSuggestionRule { Id = 44, SuggestionId = 28, SuggestionRuleId = 7, },
                    new SuggestionSuggestionRule { Id = 45, SuggestionId = 28, SuggestionRuleId = 8, },
                    new SuggestionSuggestionRule { Id = 46, SuggestionId = 28, SuggestionRuleId = 9, },
                    new SuggestionSuggestionRule { Id = 47, SuggestionId = 28, SuggestionRuleId = 10, },
                    new SuggestionSuggestionRule { Id = 48, SuggestionId = 28, SuggestionRuleId = 11, },

                    new SuggestionSuggestionRule { Id = 49, SuggestionId = 23, SuggestionRuleId = 1, },
                    new SuggestionSuggestionRule { Id = 50, SuggestionId = 23, SuggestionRuleId = 2, },
                    new SuggestionSuggestionRule { Id = 51, SuggestionId = 23, SuggestionRuleId = 3, },
                    new SuggestionSuggestionRule { Id = 52, SuggestionId = 23, SuggestionRuleId = 4, },
                    new SuggestionSuggestionRule { Id = 53, SuggestionId = 23, SuggestionRuleId = 5, },
                    new SuggestionSuggestionRule { Id = 54, SuggestionId = 23, SuggestionRuleId = 6, },
                    new SuggestionSuggestionRule { Id = 55, SuggestionId = 23, SuggestionRuleId = 7, },
                    new SuggestionSuggestionRule { Id = 56, SuggestionId = 23, SuggestionRuleId = 8, },
                    new SuggestionSuggestionRule { Id = 57, SuggestionId = 23, SuggestionRuleId = 9, },
                    new SuggestionSuggestionRule { Id = 58, SuggestionId = 23, SuggestionRuleId = 10, },
                    new SuggestionSuggestionRule { Id = 59, SuggestionId = 23, SuggestionRuleId = 11, },

                    new SuggestionSuggestionRule { Id = 60, SuggestionId = 103, SuggestionRuleId = 3, },
                    new SuggestionSuggestionRule { Id = 61, SuggestionId = 103, SuggestionRuleId = 4, },
                    new SuggestionSuggestionRule { Id = 62, SuggestionId = 103, SuggestionRuleId = 5, },
                    new SuggestionSuggestionRule { Id = 63, SuggestionId = 103, SuggestionRuleId = 6, },
                    new SuggestionSuggestionRule { Id = 64, SuggestionId = 103, SuggestionRuleId = 7, },
                    new SuggestionSuggestionRule { Id = 65, SuggestionId = 103, SuggestionRuleId = 8, },
                    new SuggestionSuggestionRule { Id = 66, SuggestionId = 103, SuggestionRuleId = 9, },
                    new SuggestionSuggestionRule { Id = 67, SuggestionId = 103, SuggestionRuleId = 10, },
                    new SuggestionSuggestionRule { Id = 68, SuggestionId = 103, SuggestionRuleId = 11, },

                    new SuggestionSuggestionRule { Id = 69, SuggestionId = 97, SuggestionRuleId = 1, },
                    new SuggestionSuggestionRule { Id = 70, SuggestionId = 97, SuggestionRuleId = 2, },
                    new SuggestionSuggestionRule { Id = 71, SuggestionId = 97, SuggestionRuleId = 3, },
                    new SuggestionSuggestionRule { Id = 72, SuggestionId = 97, SuggestionRuleId = 4, },
                    new SuggestionSuggestionRule { Id = 73, SuggestionId = 97, SuggestionRuleId = 5, },
                    new SuggestionSuggestionRule { Id = 74, SuggestionId = 97, SuggestionRuleId = 6, },
                    new SuggestionSuggestionRule { Id = 75, SuggestionId = 97, SuggestionRuleId = 7, },
                    new SuggestionSuggestionRule { Id = 76, SuggestionId = 97, SuggestionRuleId = 8, },
                    new SuggestionSuggestionRule { Id = 77, SuggestionId = 97, SuggestionRuleId = 9, },

                    new SuggestionSuggestionRule { Id = 78, SuggestionId = 7, SuggestionRuleId = 1, },
                    new SuggestionSuggestionRule { Id = 79, SuggestionId = 7, SuggestionRuleId = 2, },
                    new SuggestionSuggestionRule { Id = 80, SuggestionId = 7, SuggestionRuleId = 3, },
                    new SuggestionSuggestionRule { Id = 81, SuggestionId = 7, SuggestionRuleId = 4, },
                    new SuggestionSuggestionRule { Id = 82, SuggestionId = 7, SuggestionRuleId = 5, },
                    new SuggestionSuggestionRule { Id = 83, SuggestionId = 7, SuggestionRuleId = 6, },
                    new SuggestionSuggestionRule { Id = 84, SuggestionId = 7, SuggestionRuleId = 7, },
                    new SuggestionSuggestionRule { Id = 85, SuggestionId = 7, SuggestionRuleId = 8, },
                    new SuggestionSuggestionRule { Id = 86, SuggestionId = 7, SuggestionRuleId = 9, },
                    new SuggestionSuggestionRule { Id = 87, SuggestionId = 7, SuggestionRuleId = 10, },
                    new SuggestionSuggestionRule { Id = 88, SuggestionId = 7, SuggestionRuleId = 11, },

                    new SuggestionSuggestionRule { Id = 89, SuggestionId = 11, SuggestionRuleId = 1, },
                    new SuggestionSuggestionRule { Id = 90, SuggestionId = 11, SuggestionRuleId = 2, },
                    new SuggestionSuggestionRule { Id = 91, SuggestionId = 11, SuggestionRuleId = 3, },
                    new SuggestionSuggestionRule { Id = 92, SuggestionId = 11, SuggestionRuleId = 4, },
                    new SuggestionSuggestionRule { Id = 93, SuggestionId = 11, SuggestionRuleId = 5, },
                    new SuggestionSuggestionRule { Id = 94, SuggestionId = 11, SuggestionRuleId = 6, },
                    new SuggestionSuggestionRule { Id = 95, SuggestionId = 11, SuggestionRuleId = 7, },
                    new SuggestionSuggestionRule { Id = 96, SuggestionId = 11, SuggestionRuleId = 8, },
                    new SuggestionSuggestionRule { Id = 97, SuggestionId = 11, SuggestionRuleId = 9, },
                    new SuggestionSuggestionRule { Id = 98, SuggestionId = 11, SuggestionRuleId = 10, },
                    new SuggestionSuggestionRule { Id = 99, SuggestionId = 11, SuggestionRuleId = 11, },

                    new SuggestionSuggestionRule { Id = 100, SuggestionId = 33, SuggestionRuleId = 1, },
                    new SuggestionSuggestionRule { Id = 101, SuggestionId = 33, SuggestionRuleId = 2, },
                    new SuggestionSuggestionRule { Id = 102, SuggestionId = 33, SuggestionRuleId = 3, },
                    new SuggestionSuggestionRule { Id = 103, SuggestionId = 33, SuggestionRuleId = 4, },
                    new SuggestionSuggestionRule { Id = 104, SuggestionId = 33, SuggestionRuleId = 5, },
                    new SuggestionSuggestionRule { Id = 105, SuggestionId = 33, SuggestionRuleId = 6, },
                    new SuggestionSuggestionRule { Id = 106, SuggestionId = 33, SuggestionRuleId = 7, },
                    new SuggestionSuggestionRule { Id = 107, SuggestionId = 33, SuggestionRuleId = 8, },
                    new SuggestionSuggestionRule { Id = 108, SuggestionId = 33, SuggestionRuleId = 9, },
                    new SuggestionSuggestionRule { Id = 109, SuggestionId = 33, SuggestionRuleId = 10, },
                    new SuggestionSuggestionRule { Id = 110, SuggestionId = 33, SuggestionRuleId = 11, },
              });
        }
    }
}
