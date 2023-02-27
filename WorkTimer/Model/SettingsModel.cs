﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkTimer.Controller;
using static System.Net.Mime.MediaTypeNames;

namespace WorkTimer.Model
{
    public partial class SettingsModel : ObservableObject
    {
        [ObservableProperty]
        private string _dataSavePath;
        
        public string LasValidDataSavePath;

        partial void OnDataSavePathChanged(string value)
        {
            DataControllers.SettingsDataController.HandleDataPathChange(value);
        }

        [ObservableProperty]
        private decimal _weeklyWorkHours;

        [ObservableProperty]
        private Dictionary<DayOfWeek, bool> _workingDays;

        [ObservableProperty]
        private bool _notifyWeeklyWorkingDone;

        [ObservableProperty]
        private TimeSpan _dailyBreakTimes;

        [ObservableProperty]
        private bool _notifyBreakTimes;

        [ObservableProperty]
        private int _workHoursUntilNotify;

        [ObservableProperty]
        private bool _allwaysDiscardTimeOnFirstLogin;

        [ObservableProperty]
        private TimerController.TimerType _autoUsedTimerOnFirstLogin;

        public SettingsModel()
        {
            SetStandardValues(false);
        }

        public void SetStandardValues(bool askBeforeChange = true)
        {
            if (askBeforeChange)
            {
                string header = "Einstellungen zurücksetzen";
                string message = $"Das zurücksetzten der Einstellungen kann nciht rückgängig gemacht werden.\nMöchten Sie wirklich alle Einstellungen zurücksetzten?";
                MessageController.ShowMessageWindow(MessageController.MessageType.YesNo, header, message);
            }
            // Standard Values
            DataSavePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\JobTimer\Data\";
            LasValidDataSavePath = DataSavePath;
            WeeklyWorkHours = 40;
            WorkingDays = new Dictionary<DayOfWeek, bool>()
            {
                { DayOfWeek.Monday, true },
                { DayOfWeek.Tuesday, true },
                { DayOfWeek.Wednesday, true },
                { DayOfWeek.Thursday, true },
                { DayOfWeek.Friday, true },
                { DayOfWeek.Saturday, false },
                { DayOfWeek.Sunday, false },
            };
            NotifyWeeklyWorkingDone = true;
            DailyBreakTimes = new TimeSpan(0, 30, 0);
            NotifyBreakTimes = true;
            WorkHoursUntilNotify = 4;
            AllwaysDiscardTimeOnFirstLogin = false;
            AutoUsedTimerOnFirstLogin = TimerController.TimerType.WorkTimer;
        }
    }
}