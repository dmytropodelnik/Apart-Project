using CloneBookingAPI.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Interfaces
{
    public interface IGenerator
    {
        string GenerateKeyCode(string key, BaseRepository repository)
        {
            return null;
        }
        string GenerateCode()
        {
            return null;
        }
    }
}
