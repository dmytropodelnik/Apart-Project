using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Diagnostics;

namespace CloneBookingAPI.Services.Timers
{
    public class ChangingEmailCodeCleaner : ICleaner
    {
        private readonly ChangingEmailCodesRepository _changingEmailRepository;

        public ChangingEmailCodeCleaner(ChangingEmailCodesRepository changingEmailRepository)
        {
            _changingEmailRepository = changingEmailRepository;
        }

        public bool ClearAllCodes()
        {
            try
            {
                _changingEmailRepository.Repository.Clear();

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

                if (_changingEmailRepository.Repository.ContainsKey(codeTuple.Item1))
                {
                    _changingEmailRepository.Repository[codeTuple.Item1].Remove(codeTuple.Item2);

                    if (_changingEmailRepository.Repository[codeTuple.Item1].Count == 0)
                    {
                        _changingEmailRepository.Repository.Remove(codeTuple.Item1);
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
