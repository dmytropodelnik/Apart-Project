using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CloneBookingAPI.Services.Database.Configurations.UserProfile
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        private readonly SaltGenerator _saltGenerator = new();
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
              new User[]
              {
                new User { Id = 1, Email = "apartproject@ukr.net", FirstName = "Admin", LastName = "Admin", DisplayName = "Admin",
                           PasswordHash =  _saltGenerator.GenerateKeyCode("12341234qwe"), SaltHash = _saltGenerator.Salt,
                           RoleId = 1, },
                new User { Id = 2, Email = "inko10092001@gmail.com", FirstName = "Admin2 FirstName", LastName = "Admin2 LastName",
                           PasswordHash =  _saltGenerator.GenerateKeyCode("12341234qwe"), SaltHash = _saltGenerator.Salt,
                           RoleId = 1, DisplayName = "Admin2", },
                new User { Id = 3, Email = "kanyesupreme@ukr.net", FirstName = "User FirstName", LastName = "Test", DisplayName = "UserTest",
                           PasswordHash =  _saltGenerator.GenerateKeyCode("12341234qwe"), SaltHash = _saltGenerator.Salt,
                           RoleId = 2, },
                new User { Id = 4, Email = "prokter222@gmail.com", FirstName = "User2 FirstName", LastName = "User2 LastName",
                           PasswordHash =  _saltGenerator.GenerateKeyCode("12341234qwe"), SaltHash = _saltGenerator.Salt,
                           RoleId = 2, DisplayName = "UserTest2", },
              });
        }
    }
}
