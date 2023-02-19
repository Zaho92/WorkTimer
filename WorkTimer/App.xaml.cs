using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using System;
using System.Windows;
using WorkTimer.Controller;
using WorkTimer.ViewModel;

namespace WorkTimer
{
    public partial class App : Application
    {
        private TaskbarIcon taskBarIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            DataController.LoadTodayData();
            //TimerController.RunWorkTimer();

            taskBarIcon = GetTaskbarIcon();
            taskBarIcon.Visibility = Visibility.Visible;

            base.OnStartup(e);

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });
            SessionController.InitSession();
        }

        private TaskbarIcon GetTaskbarIcon()
        {
            TaskbarIcon tb = (TaskbarIcon)FindResource("MyNotifyIcon") ?? throw new InvalidOperationException();
            tb.DataContext = new NotifyIconViewModel();
            return tb;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DataController.SaveTodayData();
            taskBarIcon.Dispose();
            base.OnExit(e);
        }

        public void ShowBalloon(string text, string title = "Work Timer", BalloonIcon BallonType = BalloonIcon.Info)
        {
            taskBarIcon.ShowBalloonTip(title, text, BallonType);
        }
    }
}