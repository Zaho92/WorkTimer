using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WorkTimer.Controller;

namespace WorkTimer.Model
{
    internal static class Data
    {
        public static JobTimerModel TodayJobTimer { get; set; }
        public static Dictionary<DateOnly, JobTimerModel> PH_HistoryTimerData { get; set; }
    }
}
