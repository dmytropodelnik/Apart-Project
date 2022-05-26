using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Diagnostics;

namespace CloneBookingAPI.Services.Cleaners
{
    public class SubscribeCodeCleaner : ICleaner
    {
        private readonly SubscribeCodesRepository _subscribesRepository;

        public SubscribeCodeCleaner(SubscribeCodesRepository subscribesRepository)
        {
            _subscribesRepository = subscribesRepository;
        }

        public bool ClearAllCodes()
        {
            try
            {
                _subscribesRepository.Repository.Clear();

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

                if (_subscribesRepository.Repository.ContainsKey(codeTuple.Item1))
                {
                    _subscribesRepository.Repository[codeTuple.Item1].Remove(codeTuple.Item2);

                    if (_subscribesRepository.Repository[codeTuple.Item1].Count == 0)
                    {
                        _subscribesRepository.Repository.Remove(codeTuple.Item1);
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
