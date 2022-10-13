using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkTimer.Controller;
using WorkTimer.Model;

namespace WorkTimer.ViewModel;

internal partial class TodayWorkTimeViewModel : ObservableObject
{
    [ObservableProperty]
    private string _workTimeString;
    [ObservableProperty]
    private string _breakTimeString;

    public TodayWorkTimeViewModel()
    {
        WorkTimeString = GetTimeStringFormSeconds(Data.TodayJobTimer.WorkTime.Seconds);
        BreakTimeString = GetTimeStringFormSeconds(Data.TodayJobTimer.BreakTime.Seconds);
        Data.TodayJobTimer.WorkTime.PropertyChanged += SecondsCounter_PropertyChanged;
        Data.TodayJobTimer.BreakTime.PropertyChanged += SecondsCounter_PropertyChanged;
    }

    private void SecondsCounter_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SecondsCounter.Seconds))
        {
            switch (TimerController.RunningTimer)
            {
                case TimerController.TimerType.WorkTimer:
                    WorkTimeString = GetTimeStringFormSeconds((sender as SecondsCounter)?.Seconds ?? 0);
                    break;
                case TimerController.TimerType.BreakTimer:
                    BreakTimeString = GetTimeStringFormSeconds((sender as SecondsCounter)?.Seconds ?? 0);
                    break;
                default:
                    break;
            }
        }
    }

    private string GetTimeStringFormSeconds(int secondsCount)
    {
        return new TimeSpan(0, 0, secondsCount).ToString("c");
    }
}