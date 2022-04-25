using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Threading;

namespace CloneBookingAPI.Services.Timers
{
    public class ChangingEmailCodeCleanTimer : ITimer
    {
        private readonly ChangingEmailCodesRepository _changingEmailRepository;
        private ChangingEmailCodeCleaner _changingEmailCleaner;

        private readonly int _CODE_ALIVE_TIME;

        public ChangingEmailCodeCleanTimer(
            ChangingEmailCodesRepository changingEmailRepository,
            IConfiguration configuration)
        {
            _changingEmailRepository = changingEmailRepository;
            _changingEmailCleaner = new ChangingEmailCodeCleaner(_changingEmailRepository);

            _CODE_ALIVE_TIME = int.Parse(configuration["CodeAliveTimes:ChangingEmailCodeAliveTime"]);
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
