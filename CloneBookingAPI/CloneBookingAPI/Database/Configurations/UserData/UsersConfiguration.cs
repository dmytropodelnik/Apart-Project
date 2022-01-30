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
                           Password =  Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123")))  },
                new User { Id = 2, Email = "apartproject@ukr.net", FirstName = "Admin FirstName", LastName = "Admin LastName",
                           Password =  Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123123")))  },
              });
        }
    }
}
