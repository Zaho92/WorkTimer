﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using WorkTimer.Controller;

namespace WorkTimer.Model
{
    internal static class Data
    {
        public static JobTimerModel TodayJobTimer { get; set; }
        public static SecondsModel UnknownTime { get; set; }
        public static SettingsModel Settings { get; set; }

        static Data()
        {
            InitData();
        }

        public static void ClearData()
        {
            InitData();
        }

        private static void InitData()
        {
            TodayJobTimer = new JobTimerModel();
            UnknownTime = new SecondsModel();
            Settings = new SettingsModel();
        }
    }
}