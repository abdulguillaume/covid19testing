using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.Utils
{
    public class Utils
    {
        public static string toShortdate(DateTime? dt)
        {
            if (dt == null) return "-";

            return toShortdate(dt.Value);
        }

        public static string toShortdate(DateTime dt)
        {
            return dt.ToString("dd/MMM/yy");
        }

        public static string time12Hr(TimeSpan t) {
            DateTime dt = DateTime.Now.Date;
            return (dt + t).ToString("hh:mm tt");

        }

        public static string time12Hr(TimeSpan? t)
        {
            if (t == null) return "-";
            return time12Hr(t.Value);
        }
    }
}
