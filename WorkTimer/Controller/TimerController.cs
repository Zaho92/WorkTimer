using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkTimer.Model;

namespace WorkTimer.Controller
{
    internal static class TimerController
    {
        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        public enum TimerType
        {
            None,
            UnknownTimer,
            WorkTimer,
            BreakTimer
        }

        private static TimerType _runningTimer = TimerType.None;

        public static TimerType RunningTimer
        {
            get
            {
                return _runningTimer;
            }
            private set
            {
                if (value != _runningTimer)
                {
                    _runningTimer = value;
                    NotifyStaticPropertyChanged();
                }
            }
        }

        public static void RunWorkTimer()
        {
            StartTimer(TimerType.WorkTimer);
        }

        public static void RunBreakTimer()
        {
            StartTimer(TimerType.BreakTimer);
        }

        public static void RunUnknownTimer()
        {
            StartTimer(TimerType.UnknownTimer);
        }

        public static void StopAllTimers()
        {
            Data.TodayJobTimer.WorkTime.Pause();
            Data.TodayJobTimer.BreakTime.Pause();
            Data.UnknownTime.Pause();
            RunningTimer = TimerType.None;
        }

        private static void StartTimer(TimerType timer)
        {
            if (RunningTimer == timer) return;
            StopAllTimers();
            switch (timer)
            {
                case TimerType.WorkTimer:
                    Data.TodayJobTimer.WorkTime.Run();
                    break;

                case TimerType.BreakTimer:
                    Data.TodayJobTimer.BreakTime.Run();
                    break;

                case TimerType.UnknownTimer:
                    Data.UnknownTime.Run();
                    break;
            }
            RunningTimer = timer;
        }
    }
}