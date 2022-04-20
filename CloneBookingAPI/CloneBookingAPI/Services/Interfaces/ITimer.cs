using System.Timers;

namespace CloneBookingAPI.Services.Interfaces
{
    public interface ITimer
    {
        bool SetTimer((string key, string code) codeTuple)
        {
            return false;
        }
        bool DeleteTimer();
    }
}
