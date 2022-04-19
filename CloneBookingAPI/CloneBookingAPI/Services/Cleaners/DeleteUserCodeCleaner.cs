using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Diagnostics;

namespace CloneBookingAPI.Services.Timers
{
    public class DeleteUserCodeCleaner : ICleaner
    {
        private readonly DeleteUserCodesRepository _deleteUserRepository;

        public DeleteUserCodeCleaner(DeleteUserCodesRepository deleteUserRepository)
        {
            _deleteUserRepository = deleteUserRepository;
        }

        public bool ClearAllCodes()
        {
            try
            {
                _deleteUserRepository.Repository.Clear();

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

                if (_deleteUserRepository.Repository.ContainsKey(codeTuple.Item1))
                {
                    _deleteUserRepository.Repository[codeTuple.Item1].Remove(codeTuple.Item2);

                    if (_deleteUserRepository.Repository[codeTuple.Item1].Count == 0)
                    {
                        _deleteUserRepository.Repository.Remove(codeTuple.Item1);
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
