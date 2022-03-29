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
                  new FileModel { Id = 1, Name = "YSJu1Nj3YEKYuc+MC4TSlRWPvtybYtnfFT6dHXPt5k=.png", Path = "/files/YSJu1Nj3YEKYuc+MC4TSlRWPvtybYtnfFT6dHXPt5k=.png", },
                  new FileModel { Id = 2, Name = "N6TTPt7raEBYeOcLAFMjL1mWb5ip+Kt9YXHddBJ5+A=.png", Path = "/files/N6TTPt7raEBYeOcLAFMjL1mWb5ip+Kt9YXHddBJ5+A=.png", },
                  new FileModel { Id = 3, Name = "AyFnP96S870Z5dpsREvobcCqkk9WxRqZaqrlwDD+aU=.png", Path = "/files/AyFnP96S870Z5dpsREvobcCqkk9WxRqZaqrlwDD+aU=.png", },
                  new FileModel { Id = 4, Name = "PMPvpvziKwjaiBm+NJOnumepEhpdkEpmwZX+y8uGxM=.png", Path = "/files/PMPvpvziKwjaiBm+NJOnumepEhpdkEpmwZX+y8uGxM=.png", },
                  new FileModel { Id = 5, Name = "JonaGiXS6Fy5vFAhFXV6yi4gF0QAjlGfAKt1tLfNH4=.png", Path = "/files/JonaGiXS6Fy5vFAhFXV6yi4gF0QAjlGfAKt1tLfNH4=.png", },
                  new FileModel { Id = 6, Name = "hunLamNewdFFiIJuD2jTaHhnD62OU2EwqFp4lao+qNY=.png", Path = "/files/hunLamNewdFFiIJuD2jTaHhnD62OU2EwqFp4lao+qNY=.png", },
                  new FileModel { Id = 7, Name = "U4h32cGZhMmt59xHBhyIkx+2qJ6x2cs+23U9c94ugcc=.png", Path = "/files/U4h32cGZhMmt59xHBhyIkx+2qJ6x2cs+23U9c94ugcc=.png", },
                  new FileModel { Id = 8, Name = "89NTq1CzGc3AnN8+m2RNyIzkfP33nOm96VYeeoJmbBQ=.png", Path = "/files/89NTq1CzGc3AnN8+m2RNyIzkfP33nOm96VYeeoJmbBQ=.png", },
                  new FileModel { Id = 9, Name = "H8v3dNgasrnuIthKRth17pFIb+3bfi7j09YDODmk4l4=.png", Path = "/files/H8v3dNgasrnuIthKRth17pFIb+3bfi7j09YDODmk4l4=.png", },
                  new FileModel { Id = 10, Name = "pBWV7R2w3omzP+Kv5PxT627LiqV3evixcVyN4B9NovI=.png", Path = "/files/pBWV7R2w3omzP+Kv5PxT627LiqV3evixcVyN4B9NovI=.png", },
                  new FileModel { Id = 11, Name = "9FXre4iyTJ6PR2kItwfneUxvLFxi8bN402K2VcsZ4dE=.png", Path = "/files/9FXre4iyTJ6PR2kItwfneUxvLFxi8bN402K2VcsZ4dE=.png", },
                  new FileModel { Id = 12, Name = "1rpHDSXaRvlmmbFewuByZ5l02dD0lY99Nfm5TYu9Vdo=.png", Path = "/files/1rpHDSXaRvlmmbFewuByZ5l02dD0lY99Nfm5TYu9Vdo=.png", },
                  new FileModel { Id = 13, Name = "+IAx1JqqRxZudz0ReeJAGz76F5LKfzo5paBh7HCSBc=.png", Path = "/files/+IAx1JqqRxZudz0ReeJAGz76F5LKfzo5paBh7HCSBc=.png", },
                  new FileModel { Id = 14, Name = "FnsrvbI5NPfBBa+wcpa85l1orRZIdwppeOkm1EA5RiA=.png", Path = "/files/FnsrvbI5NPfBBa+wcpa85l1orRZIdwppeOkm1EA5RiA=.png", },
                  new FileModel { Id = 15, Name = "MwDbR+apJTYsCV9tUP4WPs8W0EqEiqG5m5wrca5SZsw=.png", Path = "/files/MwDbR+apJTYsCV9tUP4WPs8W0EqEiqG5m5wrca5SZsw=.png", },
                  new FileModel { Id = 16, Name = "1cDGXpJthPlZ+7Jk7++tTOYNDO0NdvQp6vtYFcpEeE=.png", Path = "/files/1cDGXpJthPlZ+7Jk7++tTOYNDO0NdvQp6vtYFcpEeE=.png", },
              });
        }
    }
}
