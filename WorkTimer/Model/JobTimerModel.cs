using System;
using WorkTimer.Controller;

namespace WorkTimer.Model
{
    public class JobTimerModel
    {
        public DateTime Date { get; set; }
        public SecondsCounter WorkTime { get; set; }
        public SecondsCounter BreakTime { get; set; }

        public JobTimerModel()
        {
            Date = DateTime.Today;
            WorkTime = new SecondsCounter();
            BreakTime = new SecondsCounter();
        }
    }
}