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
                  new FileModel { Id = 1, Name = "YSJu1Nj3YEKYuc+MC4TSlRWPvtybYtnfFT6dHXPt5k=.png", Path = "files/YSJu1Nj3YEKYuc+MC4TSlRWPvtybYtnfFT6dHXPt5k=.png", },
                  new FileModel { Id = 2, Name = "N6TTPt7raEBYeOcLAFMjL1mWb5ip+Kt9YXHddBJ5+A=.png", Path = "/files/N6TTPt7raEBYeOcLAFMjL1mWb5ip+Kt9YXHddBJ5+A=.png", },
              });
        }
    }
}
