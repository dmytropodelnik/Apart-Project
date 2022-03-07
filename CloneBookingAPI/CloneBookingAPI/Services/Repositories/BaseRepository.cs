using CloneBookingAPI.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace CloneBookingAPI.Services.Repositories
{
    public class BaseRepository : IRepository
    {
        public Dictionary<string, List<string>> Repository { get; } = new();

        //public bool IsValueExists(string code)
        //{
        //    foreach (var item in Repository)
        //    {
        //        if (item.Value == code)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public bool IsValueCorrect(string key, string code)
        {
            //foreach (var item in Repository)
            //{
            //    if (item.Key == key && item.Value == code)
            //    {
            //        return true;
            //    }
            //}
            //return false;

            if (Repository.ContainsKey(key))
            {
                if (Repository[key].Contains(code))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
