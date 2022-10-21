using System;
using System.Timers;

namespace WorkTimer.Services
{
    internal static class SecondsNotifier
    {
        private static readonly Timer Timer;

        public static event EventHandler<DateTime> SecondTick;

        static SecondsNotifier()
        {
            Timer = new Timer(1000);
            Timer.Elapsed += (s, e) =>
            {
                OnSecondTick(e.SignalTime);
            };
            Timer.Start();
        }

        private static void OnSecondTick(DateTime signal)
        {
            SecondTick?.Invoke(null, signal);
        }
    }
}