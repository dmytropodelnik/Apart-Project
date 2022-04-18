using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Diagnostics;
using System.Threading;

namespace CloneBookingAPI.Services.Timers
{
    public class DeleteUserCleanTimer : ITimer
    {
        private readonly DeleteUserCodesRepository _deleteRepository;
        private DeleteUserCodeCleaner _deleteUserCleaner;

        private const int _CODE_ALIVE_TIME = 100000;

        public DeleteUserCleanTimer(DeleteUserCodesRepository deleteRepository)
        {
            _deleteRepository = deleteRepository;
            _deleteUserCleaner = new DeleteUserCodeCleaner(_deleteRepository);
        }

        public bool SetTimer((string key, string code) codeTuple)
        {
            try
            {
                // set a callback
                TimerCallback tm = new TimerCallback(_deleteUserCleaner.ClearCode);
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
