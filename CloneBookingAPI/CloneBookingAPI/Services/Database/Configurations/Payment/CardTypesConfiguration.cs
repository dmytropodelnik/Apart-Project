using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Payment
{
    public class CardTypesConfiguration : IEntityTypeConfiguration<CardType>
    {
        public void Configure(EntityTypeBuilder<CardType> builder)
        {
            builder.HasData(
              new CardType[]
              {

              });
        }
    }
}
