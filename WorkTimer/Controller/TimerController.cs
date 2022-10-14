using WorkTimer.Model;

namespace WorkTimer.Controller
{
    internal static class TimerController
    {
        public enum TimerType
        {
            None,
            UnknownTimer,
            WorkTimer,
            BreakTimer
        }

        public static TimerType RunningTimer { get; private set; } = TimerType.None;

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

        private static void StartTimer(TimerType timer)
        {
            if (RunningTimer == timer) return;
            Data.TodayJobTimer.WorkTime.Pause();
            Data.TodayJobTimer.BreakTime.Pause();
            Data.UnknownTime.Pause();
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