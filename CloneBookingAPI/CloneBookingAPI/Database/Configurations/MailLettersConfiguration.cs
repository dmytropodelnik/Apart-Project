using CloneBookingAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloneBookingAPI.Database.Configurations
{
    public class MailLettersConfiguration : IEntityTypeConfiguration<MailLetter>
    {
        public void Configure(EntityTypeBuilder<MailLetter> builder)
        {
            builder.Property(l => l.SentCount).HasDefaultValue(1);

            builder.HasData(
              new[]
              {
                  new MailLetter { Id = 1, Title = "Test title 1", Text = "Test letter text 1", SendingDate = DateTime.UtcNow, ReceiversAmount = 15, SenderId = 1, },
                  new MailLetter { Id = 2, Title = "Test title 2", Text = "Test letter text 2", SendingDate = DateTime.UtcNow, ReceiversAmount = 25, SenderId = 1, },
                  new MailLetter { Id = 3, Title = "Test title 3", Text = "Test letter text 3", SendingDate = DateTime.UtcNow, ReceiversAmount = 10, SenderId = 1, },
                  new MailLetter { Id = 4, Title = "Test title 4", Text = "Test letter text 4", SendingDate = DateTime.UtcNow, ReceiversAmount = 12, SenderId = 2, },
                  new MailLetter { Id = 5, Title = "Test title 5", Text = "Test letter text 5", SendingDate = DateTime.UtcNow, ReceiversAmount = 6, SenderId = 2, },
              });
        }
    }
}
