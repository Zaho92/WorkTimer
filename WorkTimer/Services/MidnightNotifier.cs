using Microsoft.Win32;
using System;
using System.Timers;

namespace WorkTimer.Services
{
    internal static class MidnightNotifier
    {
        private static readonly Timer Timer;

        public static event EventHandler<DateTime> DayChanged;

        static MidnightNotifier()
        {
            Timer = new Timer(GetSleepTime());
            Timer.Elapsed += (s, e) =>
            {
                OnDayChanged(e.SignalTime);
                Timer.Interval = GetSleepTime();
            };
            Timer.Start();

            SystemEvents.TimeChanged += OnSystemTimeChanged;
        }

        private static double GetSleepTime()
        {
            var midnightTonight = DateTime.Today.AddDays(1);
            var differenceInMilliseconds = (midnightTonight - DateTime.Now).TotalMilliseconds;
            return differenceInMilliseconds;
        }

        private static void OnDayChanged(DateTime signal)
        {
            DayChanged?.Invoke(null, signal);
        }

        private static void OnSystemTimeChanged(object sender, EventArgs e)
        {
            Timer.Interval = GetSleepTime();
        }
    }
}