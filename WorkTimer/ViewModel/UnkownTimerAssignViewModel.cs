using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using WorkTimer.Controller;
using WorkTimer.Model;

namespace WorkTimer.ViewModel
{
    public partial class UnkownTimerAssignViewModel : ObservableObject
    {
        public enum AssignTypes
        {
            DropTime,
            AddToWorkTime,
            AddToBreakTime,
            AssignManually
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ShowManualAssignSettings))]
        private AssignTypes _assignMethodType;

        public bool ShowManualAssignSettings => AssignMethodType == AssignTypes.AssignManually;

        [ObservableProperty]
        private TimeSpan _assignedWorkTime;

        [ObservableProperty]
        private TimeSpan _assignedBreakTime;

        [ObservableProperty]
        private TimeSpan _unassignedUnknownTime;

        public TimeSpan MaxWorkTimeAssignment => new TimeSpan(0, 0, Data.UnknownTime.Seconds) - AssignedBreakTime;
        public TimeSpan MaxBreakTimeAssignment => new TimeSpan(0, 0, Data.UnknownTime.Seconds) - AssignedWorkTime;

        private readonly Window parentWindow;

        public UnkownTimerAssignViewModel(Window _parentWindow)
        {
            parentWindow = _parentWindow;
            AssignMethodType = AssignTypes.DropTime;
            Data.UnknownTime.PropertyChanged += UnknownTime_PropertyChanged;
        }

        private void UnknownTime_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateUnknownTime();
        }

        [RelayCommand]
        public void TimeSpanValueChanged()
        {
            UpdateUnknownTime();
        }

        private void UpdateUnknownTime()
        {
            UnassignedUnknownTime = new TimeSpan(0, 0, Data.UnknownTime.Seconds).Add(new TimeSpan(-1 * (AssignedWorkTime.Ticks + AssignedBreakTime.Ticks)));
            OnPropertyChanged(nameof(MaxWorkTimeAssignment));
            OnPropertyChanged(nameof(MaxBreakTimeAssignment));
        }

        [RelayCommand]
        public void ApplyAndWork()
        {
            AssignByType();
            TimerController.RunWorkTimer();
            Close();
        }

        [RelayCommand]
        public void ApplyAndBreak()
        {
            AssignByType();
            TimerController.RunBreakTimer();
            Close();
        }

        private void AssignByType()
        {
            switch (AssignMethodType)
            {
                case AssignTypes.AddToWorkTime:
                    Data.TodayJobTimer.WorkTime.Seconds += Data.UnknownTime.Seconds;
                    break;

                case AssignTypes.AddToBreakTime:
                    Data.TodayJobTimer.BreakTime.Seconds += Data.UnknownTime.Seconds;
                    break;

                case AssignTypes.AssignManually:
                    Data.TodayJobTimer.WorkTime.Seconds += Convert.ToInt32(AssignedWorkTime.TotalSeconds);
                    Data.TodayJobTimer.BreakTime.Seconds += Convert.ToInt32(AssignedBreakTime.TotalSeconds);
                    break;

                default:
                    break;
            }
            Data.UnknownTime = new SecondsModel();
        }

        private void Close()
        {
            parentWindow?.Close();
        }
    }
}