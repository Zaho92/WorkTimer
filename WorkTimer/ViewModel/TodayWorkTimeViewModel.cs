using CommunityToolkit.Mvvm.ComponentModel;
using WorkTimer.Controller;
using WorkTimer.Helpers;
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
        WorkTimeString = Helper.GetTimeStringFormSeconds(Data.TodayJobTimer.WorkTime.Seconds);
        BreakTimeString = Helper.GetTimeStringFormSeconds(Data.TodayJobTimer.BreakTime.Seconds);
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
                    WorkTimeString = Helper.GetTimeStringFormSeconds((sender as SecondsCounter)?.Seconds ?? 0);
                    break;

                case TimerController.TimerType.BreakTimer:
                    BreakTimeString = Helper.GetTimeStringFormSeconds((sender as SecondsCounter)?.Seconds ?? 0);
                    break;

                default:
                    break;
            }
        }
    }
}