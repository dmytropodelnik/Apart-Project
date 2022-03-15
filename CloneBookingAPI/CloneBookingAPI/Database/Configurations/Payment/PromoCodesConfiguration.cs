using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloneBookingAPI.Services.Database.Configurations.Payment
{
    public class PromoCodesConfiguration : IEntityTypeConfiguration<PromoCode>
    {
        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            builder.Property(p => p.IsActive).HasDefaultValue(true);

            builder.HasData(
              new PromoCode[]
              {
                new PromoCode { Id = 1, Code = "test", GeneratingDate = DateTime.UtcNow.AddDays(1), ExpirationDate = DateTime.UtcNow.AddDays(3),
                    IsActive = true, PercentDiscount = 20, },
              });
        }
    }
}
