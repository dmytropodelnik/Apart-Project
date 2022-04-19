using System.Timers;

namespace CloneBookingAPI.Services.Interfaces
{
    public interface ICleaner
    {
        public static ElapsedEventHandler ClearCode(object data, ElapsedEventArgs e)
        {
            return null;
        }

        public bool ClearAllCodes();
    }
}
