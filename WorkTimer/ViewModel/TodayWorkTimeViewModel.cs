using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;
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
        WorkTimeString = Data.TodayJobTimer.WorkTime.SecondsAsTimeString;
        BreakTimeString = Data.TodayJobTimer.BreakTime.SecondsAsTimeString;
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
                    WorkTimeString = (sender as SecondsCounter)?.SecondsAsTimeString;
                    break;

                case TimerController.TimerType.BreakTimer:
                    BreakTimeString = (sender as SecondsCounter)?.SecondsAsTimeString;
                    break;

                default:
                    break;
            }
        }
    }
}