using System;
using WorkTimer.Model;
using WorkTimer.Services;

namespace WorkTimer.Controller
{
    public static class TimerController
    {
        public enum TimerType
        {
            None,
            UnknownTimer,
            WorkTimer,
            BreakTimer
        }

        private static TimerType _runningTimer;

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
                    OnRunningTimerChanged();
                }
            }
        }

        public static event EventHandler<TimerType> RunningTimerChanged;

        private static void OnRunningTimerChanged()
        {
            RunningTimerChanged?.Invoke(null, _runningTimer);
        }

        static TimerController()
        {
            _runningTimer = TimerType.None;
            SecondsNotifier.SecondTick += SecondsNotifier_SecondTick;
        }

        private static void SecondsNotifier_SecondTick(object? sender, DateTime e)
        {
            switch (_runningTimer)
            {
                case TimerType.WorkTimer:
                    Data.TodayJobTimer.WorkTime.Seconds++;
                    break;

                case TimerType.BreakTimer:
                    Data.TodayJobTimer.BreakTime.Seconds++;
                    break;

                case TimerType.UnknownTimer:
                    Data.UnknownTime.Seconds++;
                    break;
            }
        }

        public static void RunTimer(TimerType type)
        {
            RunningTimer = type;
        }

        public static void RunBreakTimer()
        {
            RunningTimer = TimerType.BreakTimer;
        }

        public static void RunUnknownTimer()
        {
            RunningTimer = TimerType.UnknownTimer;
        }

        public static void RunWorkTimer()
        {
            RunningTimer = TimerType.WorkTimer;
        }

        public static void StopAllTimers()
        {
            RunningTimer = TimerType.None;
        }
    }
}