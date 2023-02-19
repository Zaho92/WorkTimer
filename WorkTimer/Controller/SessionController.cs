using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimer.View.Windows;

namespace WorkTimer.Controller
{
    internal static class SessionController
    {
        internal static void InitSession()
        {
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
            StartStatupSession();
        }

        private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                TimerController.RunUnknownTimer();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                ShowUnkownTimerAssignWindow();
            }
        }

        private static void StartStatupSession()
        {
            TimerController.RunUnknownTimer();
            ShowUnkownTimerAssignWindow();
        }

        private static void ShowUnkownTimerAssignWindow()
        {
            new UnkownTimerAssignWindow().ShowDialog();
        }
    }
}