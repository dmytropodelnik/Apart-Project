using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Threading;

namespace CloneBookingAPI.Services.Timers
{
    public class ResetPasswordCodeCleanTimer : ITimer
    {
        private readonly ResetPasswordCodesRepository _resetPasswordRepository;
        private ResetPasswordCodeCleaner _resetPasswordCleaner;

        private readonly int _CODE_ALIVE_TIME;

        public ResetPasswordCodeCleanTimer(
            ResetPasswordCodesRepository resetPasswordRepository,
            IConfiguration configuration)
        {
            _resetPasswordRepository = resetPasswordRepository;
            _resetPasswordCleaner = new ResetPasswordCodeCleaner(_resetPasswordRepository);

            _CODE_ALIVE_TIME = int.Parse(configuration["CodeAliveTimes:ResetPasswordCodeAliveTime"]);
        }

        public bool SetTimer((string key, string code) codeTuple)
        {
            try
            {
                // устанавливаем метод обратного вызова
                TimerCallback tm = new TimerCallback(_resetPasswordCleaner.ClearCode);
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
