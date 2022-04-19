using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Diagnostics;
using System.Threading;

namespace CloneBookingAPI.Services.Timers
{
    public class ChangingEmailCleanTimer : ITimer
    {
        private readonly ChangingEmailCodesRepository _changingEmailRepository;
        private ChangingEmailCodeCleaner _changingEmailCleaner;

        private const int _CODE_ALIVE_TIME = 600000;

        public ChangingEmailCleanTimer(ChangingEmailCodesRepository changingEmailRepository)
        {
            _changingEmailRepository = changingEmailRepository;
            _changingEmailCleaner = new ChangingEmailCodeCleaner(_changingEmailRepository);
        }

        public bool SetTimer((string key, string code) codeTuple)
        {
            try
            {
                // set a callback
                TimerCallback tm = new TimerCallback(_changingEmailCleaner.ClearCode);
                // create a timer
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
