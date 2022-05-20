using CloneBookingAPI.Services.POCOs;
using System.Threading.Tasks;

namespace CloneBookingAPI.Interfaces
{
    public interface IEmailSender
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        async Task<bool> SendEmailAsync(string email, string subject, string message)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return false;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        async Task<bool> SendEmailAsync(MailLetterPoco letter)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return false;
        }
    }
}
