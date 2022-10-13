using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using System;
using System.Windows;
using WorkTimer.Controller;
using WorkTimer.ViewModel;

namespace WorkTimer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon taskBarIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            DataController.LoadData();
            TimerController.RunWorkTimer();

            taskBarIcon = GetTaskbarIcon();
            taskBarIcon.Visibility = Visibility.Visible;
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;

            base.OnStartup(e);

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });
        }

        private TaskbarIcon GetTaskbarIcon()
        {
            TaskbarIcon tb = (TaskbarIcon)FindResource("MyNotifyIcon") ?? throw new InvalidOperationException();
            tb.DataContext = new NotifyIconViewModel();
            return tb;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DataController.SaveData();
            taskBarIcon.Dispose();
            base.OnExit(e);
        }

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                TimerController.RunBreakTimer();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                TimerController.RunWorkTimer();
            }
        }

        public void ShowBalloon(string text, string title = "Work Timer", BalloonIcon BallonType = BalloonIcon.Info)
        {
            taskBarIcon.ShowBalloonTip(title, text, BallonType);
        }
    }
}