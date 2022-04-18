using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.POCOs;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace CloneBookingAPI.Services.Timers
{
    public class JwtCodeCleaner : ICleaner
    {
        private readonly JwtRepository _jwtRepository;

        public JwtCodeCleaner(JwtRepository jwtRepository)
        {
            _jwtRepository = jwtRepository;
        }

        public bool ClearAllCodes()
        {
            try
            {
                _jwtRepository.Repository.Clear();

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

                if (_jwtRepository.Repository.ContainsKey(codeTuple.Item1))
                {
                    _jwtRepository.Repository[codeTuple.Item1].Remove(codeTuple.Item2);

                    if (_jwtRepository.Repository[codeTuple.Item1].Count == 0)
                    {
                        _jwtRepository.Repository.Remove(codeTuple.Item1);
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
