using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
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
