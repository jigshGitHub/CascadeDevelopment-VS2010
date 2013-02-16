using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cascade.Helpers
{
    public class DateHelper
    {
        public static DateTime GetDateWithTimings(DateTime dt)
        {
            dt = dt.AddHours(DateTime.Now.Hour);
            dt = dt.AddMinutes(DateTime.Now.Minute);
            dt = dt.AddSeconds(DateTime.Now.Second);
            dt = dt.AddMilliseconds(DateTime.Now.Millisecond);
            return dt;
        }
    }
}
