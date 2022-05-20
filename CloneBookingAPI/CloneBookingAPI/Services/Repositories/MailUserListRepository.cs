using CloneBookingAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CloneBookingAPI.Services.Repositories
{
    public class MailUserListRepository : IRepository
    {
        private List<string> _subscribers = new();

        public List<string> Subscribers
        {
            get => _subscribers;
            set => _subscribers = value;
        }

        public bool IsValueExists(string code)
        {
            try
            {
                if (_subscribers.Contains(code))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
