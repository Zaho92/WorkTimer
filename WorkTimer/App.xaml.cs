using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using WorkTimer.View.ViewModel;
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

            base.OnStartup(e);

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });

            //TodayTimeWindow app = new TodayTimeWindow();
            //TodayTimeViewModel context = new TodayTimeViewModel();
            //app.DataContext = context;
            //app.Show();
        }

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            //if (e.Reason == SessionSwitchReason.SessionLock)
            //{
            //    tvm?.StopWorking();
            //}
            //else if (e.Reason == SessionSwitchReason.SessionUnlock)
            //{
            //    tvm?.StartWorking();
            //}
        }
    }
}
