using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations
{
    public class FileModelConfiguration : IEntityTypeConfiguration<FileModel>
    {
        public void Configure(EntityTypeBuilder<FileModel> builder)
        {
            builder.HasData(
              new FileModel[]
              {
                  new FileModel { Id = 1, Name = "1pBnHFqQXE7qBTAZnKfokySQ3emLkc+eFl6zirsp3SM=.jpg", Path = "/files/1pBnHFqQXE7qBTAZnKfokySQ3emLkc+eFl6zirsp3SM=.jpg", },
              });
        }
    }
}
