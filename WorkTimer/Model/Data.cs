using System;
using System.Collections.Generic;
using WorkTimer.Controller;

namespace WorkTimer.Model
{
    internal static class Data
    {
        public static JobTimerModel TodayJobTimer { get; set; } = new JobTimerModel();
        public static SecondsCounter UnknownTime { get; set; } = new SecondsCounter();
        public static Dictionary<DateOnly, JobTimerModel> PH_HistoryTimerData { get; set; } = new Dictionary<DateOnly, JobTimerModel>();
    }
}