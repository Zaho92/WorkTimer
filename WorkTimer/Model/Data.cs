using System;
using System.Collections.Generic;
using WorkTimer.Controller;

namespace WorkTimer.Model
{
    internal static class Data
    {
        public static JobTimerModel TodayJobTimer { get; set; } = new JobTimerModel();
        public static SecondsModel UnknownTime { get; set; } = new SecondsModel();
        public static Dictionary<DateOnly, JobTimerModel> PH_HistoryTimerData { get; set; } = new Dictionary<DateOnly, JobTimerModel>();

        public static void ClearData()
        {
            TodayJobTimer = new JobTimerModel();
            UnknownTime = new SecondsModel();
            PH_HistoryTimerData = new Dictionary<DateOnly, JobTimerModel>();
        }
    }
}