using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Diagnostics;
using System.Threading;

namespace CloneBookingAPI.Services.Timers
{
    public class EnterCleanTimer : ITimer
    {
        private readonly EnterCodesRepository _enterRepository;
        private EnterCodeCleaner _enterCleaner;

        private const int _CODE_ALIVE_TIME = 30000;

        public EnterCleanTimer(EnterCodesRepository enterRepository)
        {
            _enterRepository = enterRepository;
            _enterCleaner = new EnterCodeCleaner(_enterRepository);
        }

        public bool SetTimer((string key, string code) codeTuple)
        {
            try
            {
                // устанавливаем метод обратного вызова
                TimerCallback tm = new TimerCallback(_enterCleaner.ClearCode);
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
