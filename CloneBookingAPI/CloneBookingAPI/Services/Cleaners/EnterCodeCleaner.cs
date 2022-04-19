using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Diagnostics;

namespace CloneBookingAPI.Services.Timers
{
    public class EnterCodeCleaner : ICleaner
    {
        private readonly EnterCodesRepository _enterRepository;

        public EnterCodeCleaner(EnterCodesRepository enterRepository)
        {
            _enterRepository = enterRepository;
        }

        public bool ClearAllCodes()
        {
            try
            {
                _enterRepository.Repository.Clear();

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

                if (_enterRepository.Repository.ContainsKey(codeTuple.Item1))
                {
                    _enterRepository.Repository[codeTuple.Item1].Remove(codeTuple.Item2);

                    if (_enterRepository.Repository[codeTuple.Item1].Count == 0)
                    {
                        _enterRepository.Repository.Remove(codeTuple.Item1);
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
