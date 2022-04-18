using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Diagnostics;

namespace CloneBookingAPI.Services.Timers
{
    public class ResetPasswordCodeCleaner : ICleaner
    {
        private readonly ResetPasswordCodesRepository _resetPasswordRepository;

        public ResetPasswordCodeCleaner(ResetPasswordCodesRepository resetPasswordRepository)
        {
            _resetPasswordRepository = resetPasswordRepository;
        }

        public bool ClearAllCodes()
        {
            try
            {
                _resetPasswordRepository.Repository.Clear();

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

                if (_resetPasswordRepository.Repository.ContainsKey(codeTuple.Item1))
                {
                    _resetPasswordRepository.Repository[codeTuple.Item1].Remove(codeTuple.Item2);

                    if (_resetPasswordRepository.Repository[codeTuple.Item1].Count == 0)
                    {
                        _resetPasswordRepository.Repository.Remove(codeTuple.Item1);
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
