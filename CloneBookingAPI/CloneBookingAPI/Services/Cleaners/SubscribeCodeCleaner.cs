using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Diagnostics;

namespace CloneBookingAPI.Services.Cleaners
{
    public class SubscriptionCodeCleaner : ICleaner
    {
        private readonly SubscriptionCodesRepository _subscriptionsRepository;

        public SubscriptionCodeCleaner(SubscriptionCodesRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
        }

        public bool ClearAllCodes()
        {
            try
            {
                _subscriptionsRepository.Repository.Clear();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
        }

        public void ClearCode(object data)
        {
            try
            {
                var codeTuple = ((string, string))data;

                if (_subscriptionsRepository.Repository.ContainsKey(codeTuple.Item1))
                {
                    _subscriptionsRepository.Repository[codeTuple.Item1].Remove(codeTuple.Item2);

                    if (_subscriptionsRepository.Repository[codeTuple.Item1].Count == 0)
                    {
                        _subscriptionsRepository.Repository.Remove(codeTuple.Item1);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
