using CloneBookingAPI.Database.Models.Suggestions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.Suggestions
{
    public class ContactDetailsConfiguration : IEntityTypeConfiguration<ContactDetails>
    {
        public void Configure(EntityTypeBuilder<ContactDetails> builder)
        {
            builder.HasData(
              new ContactDetails[]
              {
                    
              });
        }
    }
}
