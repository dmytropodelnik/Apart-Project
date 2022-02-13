using CloneBookingAPI.Interfaces;
using System.Collections.Generic;

namespace CloneBookingAPI.Services.Repositories
{
    public class JwtRepository : IRepository
    {
        public Dictionary<string, string> Repository { get; } = new();

        public bool IsValueExists(string code)
        {
            foreach (var item in Repository)
            {
                if (item.Value == code)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsValueCorrect(string key, string code)
        {
            foreach (var item in Repository)
            {
                if (item.Key == key && item.Value == code)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
