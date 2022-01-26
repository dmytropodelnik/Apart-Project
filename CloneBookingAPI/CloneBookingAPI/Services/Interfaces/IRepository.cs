using System.Collections;
using System.Collections.Generic;

namespace CloneBookingAPI.Interfaces
{
    public interface IRepository
    {
        bool IsValueExists(string code)
        {
            return false;
        }

        bool IsValueCorrect(string key, string code)
        {
            return false;
        }
    }
}
