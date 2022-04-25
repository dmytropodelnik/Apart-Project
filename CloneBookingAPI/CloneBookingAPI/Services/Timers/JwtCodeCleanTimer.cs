using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Threading;

namespace CloneBookingAPI.Services.Timers
{
    public class JwtCodeCleanTimer : ITimer
    {
        private readonly JwtRepository _jwtRepository;
        private JwtCodeCleaner _jwtCleaner;

        private readonly int _CODE_ALIVE_TIME;

        public JwtCodeCleanTimer(
            JwtRepository jwtRepository,
            IConfiguration configuration)
        {
            _jwtRepository = jwtRepository;
            _jwtCleaner = new JwtCodeCleaner(_jwtRepository);

            _CODE_ALIVE_TIME = int.Parse(configuration["CodeAliveTimes:JwtCodeAliveTime"]);
        }

        public bool SetTimer((string key, string code) codeTuple)
        {
            try
            {
                // устанавливаем метод обратного вызова
                TimerCallback tm = new TimerCallback(_jwtCleaner.ClearCode);
                // создаем таймер
                Timer timer = new Timer(tm, codeTuple, _CODE_ALIVE_TIME, -1);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
        }

        public bool DeleteTimer()
        {
            try
            {
               

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
