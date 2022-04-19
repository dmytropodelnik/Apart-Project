using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Diagnostics;

namespace CloneBookingAPI.Services.Timers
{
    public class RegistrationCodeCleaner : ICleaner
    {
        private readonly RegistrationCodesRepository _registrationRepository;

        public RegistrationCodeCleaner(RegistrationCodesRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public bool ClearAllCodes()
        {
            try
            {
                _registrationRepository.Repository.Clear();

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

                if (_registrationRepository.Repository.ContainsKey(codeTuple.Item1))
                {
                    _registrationRepository.Repository[codeTuple.Item1].Remove(codeTuple.Item2);

                    if (_registrationRepository.Repository[codeTuple.Item1].Count == 0)
                    {
                        _registrationRepository.Repository.Remove(codeTuple.Item1);
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
