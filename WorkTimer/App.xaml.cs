using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using System;
using System.Windows;
using WorkTimer.Controller;
using WorkTimer.ViewModel;

namespace WorkTimer
{
    // WORKTIMER TODOS
    //TODO Wecker funktion für Pausen/Frage Snooze/Not-Tracking (Action-Übergabe)
    //TODO PROGRAMMVERHALTEN
    //TODO  - Abfrage welcher Timer (ggf. Anrechnung des aktuellen UnknownTimer auf die gewählte Zeit)
    //TODO  - Bei Programmstart: Wenn locked dann warten auf Entsperren SONST UnknownTimer starten + Abfrage
    //TODO  - Bei Entsperrung: UnknownTimer + Abfrage
    //TODO  - Bei Sperren: UnknownTimer reset & starten
    //TODO SETTINGS
    //TODO  - Übliche Pausenzeit
    //TODO      - "Mache eine Pause" Erinnerung -> Nach X Stunden Arbeit an die Pause Erinnern / Snooze
    //TODO  - Übliche Arbeitszeit / Wochenarbeitszeit
    //TODO      - Not-Tracking Alarm -> wenn Idol während der Arbeitszeit alle X Minuten (Abschaltbar / Custom Snooze)
    //TODO HISTORIE/STATISTIK
    //TODO  - Als Dashboard mit Statistiken -> Diese Woche / Letzte Woche / Über-/Minus-Stunden usw.
    //TODO  - Detailsuche nach bestimmtem Tag/Woche/Monat (ggf. frei wählbare Datumseingrenzung?)
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