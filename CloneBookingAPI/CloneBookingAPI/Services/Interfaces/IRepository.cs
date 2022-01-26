using System.Collections;
using System.Collections.Generic;

namespace CloneBookingAPI.Interfaces
{
    public interface IRepository
    {
        bool IsCodeCorrect(string key, string code)
        {
            return false;
        }
    }
}
