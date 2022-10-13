using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using WorkTimer.Controller;
using WorkTimer.View.Windows;

namespace WorkTimer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon? tb;
        //private readonly TimerViewModel? tvm;

        protected override void OnStartup(StartupEventArgs e)
        {
            tb = (TaskbarIcon)FindResource("MyNotifyIcon") ?? throw new InvalidOperationException();
            tb.Visibility = Visibility.Visible;
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
            DataController.LoadData();
            TimerController.RunWorkTimer();
            base.OnStartup(e);

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DataController.SaveData();
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
    }
}
