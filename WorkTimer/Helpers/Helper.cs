using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimer.Helpers
{
    internal static class Helper
    {
        public static string GetTimeStringFormSeconds(int secondsCount)
        {
            return new TimeSpan(0, 0, secondsCount).ToString("c");
        }
    }
}
