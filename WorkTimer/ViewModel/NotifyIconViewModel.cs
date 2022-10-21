using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WorkTimer.Controller;
using WorkTimer.Model;
using WorkTimer.Services;
using WorkTimer.View.Windows;

namespace WorkTimer.ViewModel
{
    internal partial class NotifyIconViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _toolTipText;

        public bool CanStartWorkTimer => TimerController.RunningTimer != TimerController.TimerType.WorkTimer;
        public bool CanStartBreakTimer => TimerController.RunningTimer != TimerController.TimerType.BreakTimer;
        public bool CanStopTimers => TimerController.RunningTimer != TimerController.TimerType.None;

        public NotifyIconViewModel()
        {
            TimerController.RunningTimerChanged += TimerController_RunningTimerChanged;
            SecondsNotifier.SecondTick += SecondsNotifier_SecondTick;
        }

        private void TimerController_RunningTimerChanged(object? sender, TimerController.TimerType e)
        {
            OnPropertyChanged(nameof(CanStartWorkTimer));
            OnPropertyChanged(nameof(CanStartBreakTimer));
            OnPropertyChanged(nameof(CanStopTimers));
        }

        private void SecondsNotifier_SecondTick(object? sender, System.DateTime e)
        {
            // TODO Sekündliche Updates sind sicher nicht performant aber ein workaround solang die Interaction.Triggers nicht funktionieren
            RefreshToolTipText();
        }

        [RelayCommand]
        public void RefreshToolTipText()
        {
            string internalToolTipText = "";
            switch (TimerController.RunningTimer)
            {
                case TimerController.TimerType.WorkTimer:
                    internalToolTipText += "Du Arbeitest momentan";
                    break;

                case TimerController.TimerType.BreakTimer:
                    internalToolTipText += "Du machst momentan Pause";
                    break;

                default:
                    internalToolTipText += "Kein Timer läuft momentan";
                    break;
            }
            internalToolTipText += "\n\n";

            internalToolTipText += $"Arbeitszeit: {Data.TodayJobTimer.WorkTime.SecondsAsTimeString}\n";
            internalToolTipText += $"Pausenzeit: {Data.TodayJobTimer.BreakTime.SecondsAsTimeString}\n";

            ToolTipText = internalToolTipText;
        }

        [RelayCommand]
        public void OpenMainWindow()
        {
            if (!(Application.Current.MainWindow?.Activate() ?? false))
            {
                Application.Current.MainWindow = new MainWindow();
                Application.Current.MainWindow.Show();
            }
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

        [RelayCommand]
        public void StopAllTimers()
        {
            TimerController.StopAllTimers();
        }

        [RelayCommand]
        public void ExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}