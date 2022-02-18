using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloneBookingAPI.Services.Database.Configurations.UserProfile
{
    public class UserProfilesConfiguration : IEntityTypeConfiguration<Models.UserProfile.UserProfile>
    {
        public void Configure(EntityTypeBuilder<Models.UserProfile.UserProfile> builder)
        {
            builder.Property(up => up.RegisterDate).HasDefaultValue(DateTime.Now);
            builder.Property(up => up.LanguageId).HasDefaultValue(1);

            builder.HasData(
              new Models.UserProfile.UserProfile[]
              {
                  new Models.UserProfile.UserProfile
                  {
                      Id = 1, RegisterDate = DateTime.Now.ToUniversalTime(), GenderId = 1, AddressId = 1, CurrencyId = 1, LanguageId = 1,
                      UserId = 1, ImageId = 1,
                  },
                  new Models.UserProfile.UserProfile
                  {
                      Id = 2, RegisterDate = DateTime.Now.ToUniversalTime(), GenderId = 1, AddressId = 2, CurrencyId = 2, LanguageId = 2,
                      UserId = 2, ImageId = 2,
                  },
                  new Models.UserProfile.UserProfile
                  {
                      Id = 3, RegisterDate = DateTime.Now.ToUniversalTime(), GenderId = 1, AddressId = 3, CurrencyId = 2, LanguageId = 1,
                      UserId = 3, ImageId = 1,
                  },
                  new Models.UserProfile.UserProfile
                  {
                      Id = 4, RegisterDate = DateTime.Now.ToUniversalTime(), GenderId = 1, AddressId = 4, CurrencyId = 2, LanguageId = 2,
                      UserId = 4, ImageId = 2, 
                  },
              });
        }
    }
}
