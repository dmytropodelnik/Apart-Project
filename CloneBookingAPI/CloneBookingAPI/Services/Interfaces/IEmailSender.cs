using System.Threading.Tasks;

namespace CloneBookingAPI.Interfaces
{
    public interface IEmailSender
    {
        async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            return false;
        }
    }
}
