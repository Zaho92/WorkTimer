using System;
using System.Collections.Generic;

namespace WorkTimer.Model
{
    internal static class Data
    {
        public static JobTimerModel TodayJobTimer { get; set; }
        public static Dictionary<DateOnly, JobTimerModel> PH_HistoryTimerData { get; set; }
    }
}