using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Services.Database.Configurations.Payment
{
    public class CurrenciesConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasData(
              new Currency[]
              {
                  new Currency { Id = 1, Value = "USA Dollar", Abbreviation = "USD", BankCode = "$" },
                  new Currency { Id = 2, Value = "Euro", Abbreviation = "EUR", BankCode = "€" },
                  new Currency { Id = 3, Value = "Рубль", Abbreviation = "RUB", BankCode = "₽" },
              });
        }
    }
}
