using CloneBookingAPI.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Generators
{
    public class CodesRepository : IRepository
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
