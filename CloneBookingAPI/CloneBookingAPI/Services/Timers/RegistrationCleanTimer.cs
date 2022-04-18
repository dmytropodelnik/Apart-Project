using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading;

namespace CloneBookingAPI.Services.Timers
{
    public class RegistrationCleanTimer : ITimer
    {
        private readonly RegistrationCodesRepository _registrationRepository;
        private RegistrationCodeCleaner _registrationCleaner;

        private const int _CODE_ALIVE_TIME = 30000;

        public RegistrationCleanTimer(RegistrationCodesRepository jwtRepository)
        {
            _registrationRepository = jwtRepository;
            _registrationCleaner = new RegistrationCodeCleaner(_registrationRepository);
        }

        public bool SetTimer((string key, string code) codeTuple)
        {
            try
            {
                // устанавливаем метод обратного вызова
                TimerCallback tm = new TimerCallback(_registrationCleaner.ClearCode);
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
