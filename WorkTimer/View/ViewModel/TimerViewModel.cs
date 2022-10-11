using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WorkTimer.View.ViewModel
{
    internal class TimerViewModel : ViewModelBase
    {
        public TimerViewModel(string displayName) : base(displayName)
        {
            DispatcherTimer updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(800);
            updateTimer.Tick += new EventHandler(timer_Tick);
            updateTimer.Start();

            _swWorkToday = new Stopwatch();
            _swBreakToday = new Stopwatch();

        }

        private readonly Stopwatch _swWorkToday;
        private readonly Stopwatch _swBreakToday;

        //public event PropertyChangedEventHandler? PropertyChanged;
        public TimeSpan WorkTimeToday => _swWorkToday.Elapsed;
        public TimeSpan BreakTimeToday => _swBreakToday.Elapsed;

        private void timer_Tick(object? sender, EventArgs e)
        {
            RaisePropertyChanged("WorkTimeToday");
            RaisePropertyChanged("BreakTimeToday");
        }

        private void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, propertyName);
        }

        public void StartWorking()
        {
            _swWorkToday.Start();
            _swBreakToday.Stop();
        }

        public void StopWorking()
        {
            _swWorkToday.Stop();
            _swBreakToday.Start();
        }
    }
}
