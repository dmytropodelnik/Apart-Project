using CloneBookingAPI.Services.Cleaners;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Threading;

namespace CloneBookingAPI.Services.Timers
{
    public class SubscriptionCodeCleanTimer : ITimer
    {
        private readonly SubscriptionCodesRepository _subscriptionsRepository;
        private readonly SubscriptionCodeCleaner _subscriptionsCleaner;

        private readonly int _CODE_ALIVE_TIME;

        public SubscriptionCodeCleanTimer(
            SubscriptionCodesRepository subscriptionsRepository,
            IConfiguration configuration)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _subscriptionsCleaner = new SubscriptionCodeCleaner(_subscriptionsRepository);

            _CODE_ALIVE_TIME = int.Parse(configuration["CodeAliveTimes:SubscriptionCodeAliveTime"]);
        }

        public bool SetTimer((string key, string code) codeTuple)
        {
            try
            {
                // устанавливаем метод обратного вызова
                TimerCallback tm = new TimerCallback(_subscriptionsCleaner.ClearCode);
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
