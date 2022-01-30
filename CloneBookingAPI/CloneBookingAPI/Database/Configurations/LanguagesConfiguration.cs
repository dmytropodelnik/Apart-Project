using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class LanguagesConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasData(
              new Language[]
              {
                  new Language { Id = 1, Title = "English" },
                  new Language { Id = 2, Title = "Ukrainian" },
                  new Language { Id = 3, Title = "Russian" },
                  new Language { Id = 4, Title = "German" },
                  new Language { Id = 5, Title = "Polish" },
              });
        }
    }
}
