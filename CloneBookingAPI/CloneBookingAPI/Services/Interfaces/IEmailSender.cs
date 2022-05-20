using CloneBookingAPI.Services.POCOs;

namespace CloneBookingAPI.Services.Interfaces
{
    public interface IEmailSender
    {
        bool SendEmail(string email, string subject, string message)
        {
            return false;
        }

        bool SendEmail(MailLetterPoco letter)
        {
            return false;
        }
    }
}
