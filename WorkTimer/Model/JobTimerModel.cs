using System;
using WorkTimer.Controller;

namespace WorkTimer.Model
{
    public class JobTimerModel
    {
        public DateTime Date { get; set; }
        public SecondsModel WorkTime { get; set; }
        public SecondsModel BreakTime { get; set; }

        public JobTimerModel()
        {
            Date = DateTime.Today;
            WorkTime = new SecondsModel();
            BreakTime = new SecondsModel();
        }
    }
}