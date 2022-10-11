using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimer.Model
{
    public class TodayTimeModel : ObservableObject
    {
        private TimeSpan _todayWorkTime;
        private TimeSpan _todayBreakTime;

        public TimeSpan TodayWorkTime
        {
            get { return _todayWorkTime; }
            set
            {
                if (value!=_todayWorkTime)
                {
                    _todayWorkTime = value;
                    OnPropertyChanged("TodayWorkTime");
                }
            }
        }

        public TimeSpan TodayBreakTime
        {
            get { return _todayBreakTime; }
            set
            {
                if (value != _todayBreakTime)
                {
                    _todayBreakTime = value;
                    OnPropertyChanged("TodayBreakTime");
                }
            }
        }
    }
}
