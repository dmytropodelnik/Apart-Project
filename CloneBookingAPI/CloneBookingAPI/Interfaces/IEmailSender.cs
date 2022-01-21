using System.Threading.Tasks;

namespace CloneBookingAPI.Interfaces
{
    public interface IEmailSender
    {
        async Task SendEmailAsync(string email, string subject, string message)
        {

        }
    }
}
