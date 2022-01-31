using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CloneBookingAPI.Services.Database.Configurations.UserProfile
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            SHA256 sha256 = SHA256.Create();

            builder.HasData(
              new User[]
              {
                new User { Id = 1, Email = "apartproject@ukr.net", FirstName = "Admin", LastName = "Admin", DisplayName = "Admin",
                           Password =  Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("12341234qwe"))),
                           ProfileId = 1, RoleId = 1 },
                new User { Id = 2, Email = "inko10092001@gmail.com", FirstName = "Admin2 FirstName", LastName = "Admin2 LastName",
                           Password =  Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("12341234qwe"))),
                           ProfileId = 2, RoleId = 1, DisplayName = "Admin2" },
                new User { Id = 3, Email = "kanyesupreme@ukr.net", FirstName = "User FirstName", LastName = "Test", DisplayName = "UserTest",
                           Password =  Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("12341234qwe"))),
                           ProfileId = 3, RoleId = 2, },
                new User { Id = 4, Email = "prokter222@gmail.com", FirstName = "User2 FirstName", LastName = "User2 LastName",
                           Password =  Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("12341234qwe"))),
                           ProfileId = 4, RoleId = 2, DisplayName = "UserTest2" },
              });
        }
    }
}
