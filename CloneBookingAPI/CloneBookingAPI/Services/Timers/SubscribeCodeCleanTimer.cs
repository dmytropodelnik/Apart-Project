using CloneBookingAPI.Services.Cleaners;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Threading;

namespace CloneBookingAPI.Services.Timers
{
    public class SubscribeCodeCleanTimer : ITimer
    {
        private readonly SubscribeCodesRepository _subscribesRepository;
        private SubscribeCodeCleaner _subscribesCleaner;

        private readonly int _CODE_ALIVE_TIME;

        public SubscribeCodeCleanTimer(
            SubscribeCodesRepository enterRepository,
            IConfiguration configuration)
        {
            _subscribesRepository = enterRepository;
            _subscribesCleaner = new SubscribeCodeCleaner(_subscribesRepository);

            _CODE_ALIVE_TIME = int.Parse(configuration["CodeAliveTimes:SubscribeCodeAliveTime"]);
        }

        public bool SetTimer((string key, string code) codeTuple)
        {
            try
            {
                // устанавливаем метод обратного вызова
                TimerCallback tm = new TimerCallback(_subscribesCleaner.ClearCode);
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
