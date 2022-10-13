using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;
using WorkTimer.Controller;
using WorkTimer.Helpers;
using WorkTimer.Model;

namespace WorkTimer.ViewModel
{
    internal partial class NotifyIconViewModel : ObservableObject
    {
        [ObservableProperty] 
        private string _toolTipText;
        [ObservableProperty]
        private bool _canStartWorkTimer;
        [ObservableProperty]
        private bool _canStartBreakTimer;


        public NotifyIconViewModel()
        {
            ToolTipText = GetToolTipText();
            CanStartWorkTimer = TimerController.RunningTimer != TimerController.TimerType.WorkTimer;
            CanStartBreakTimer = TimerController.RunningTimer != TimerController.TimerType.BreakTimer;
            Data.TodayJobTimer.WorkTime.PropertyChanged += SecondsCounter_PropertyChanged;
            Data.TodayJobTimer.BreakTime.PropertyChanged += SecondsCounter_PropertyChanged;
        }
        
        private void SecondsCounter_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SecondsCounter.Seconds))
            {
                ToolTipText = GetToolTipText();
                CanStartWorkTimer = TimerController.RunningTimer != TimerController.TimerType.WorkTimer;
                CanStartBreakTimer = TimerController.RunningTimer != TimerController.TimerType.BreakTimer;
            }
        }

        private string GetToolTipText()
        {
            string internalToolTipText = "";
            switch (TimerController.RunningTimer)
            {
                case TimerController.TimerType.WorkTimer:
                    internalToolTipText += "Der Arbeits-Timer läuft momentan";
                    break;
                case TimerController.TimerType.BreakTimer:
                    internalToolTipText += "Der Pause-Timer läuft momentan";
                    break;
                default:
                    internalToolTipText += "Kein Timer läuft momentan";
                    break;
            }
            internalToolTipText += "\n\n";

            internalToolTipText += $"Arbeitszeit: {Helper.GetTimeStringFormSeconds(Data.TodayJobTimer.WorkTime.Seconds)}\n";
            internalToolTipText += $"Pausenzeit: {Helper.GetTimeStringFormSeconds(Data.TodayJobTimer.BreakTime.Seconds)}\n";

            return internalToolTipText;
        }

        [RelayCommand]
        public void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        [RelayCommand]
        public void StartWorkTimer()
        {
            TimerController.RunWorkTimer();
        }

        [RelayCommand]
        public void StartBreakTimer()
        {
            TimerController.RunBreakTimer();
        }

    }
}
