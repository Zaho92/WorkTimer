using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkTimer.Model;

namespace WorkTimer.Controller
{
    internal static class TimerController
    {
        public enum TimerType
        {
            None,
            WorkTimer,
            BreakTimer
        }
         
        public static TimerType RunningTimer { get; private set; } = TimerType.None;

        public static void RunWorkTimer()
        {
            if (RunningTimer != TimerType.WorkTimer)
            {
                Data.TodayJobTimer.BreakTime.Pause();
                Data.TodayJobTimer.WorkTime.Run();
                RunningTimer = TimerType.WorkTimer;
            }
        }

        public static void RunBreakTimer()
        {
            if (RunningTimer != TimerType.BreakTimer)
            {
                Data.TodayJobTimer.WorkTime.Pause();
                Data.TodayJobTimer.BreakTime.Run();
                RunningTimer = TimerType.BreakTimer;
            }
        }
    }
}
