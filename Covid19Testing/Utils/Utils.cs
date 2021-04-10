using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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


        public static string ExcelColumnIndexToName(int Index)
        {
            string range = "";
            if (Index < 0) return range;
            for (int i = 1; Index + i > 0; i = 0)
            {
                range = ((char)(65 + Index % 26)).ToString() + range;
                Index /= 26;
            }
            if (range.Length > 1) range = ((char)((int)range[0] - 1)).ToString() + range.Substring(1);
            return range;
        }

        public static string SlugifyText(string Text)
        {
            Text = Text.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(Text);
            Text = Encoding.ASCII.GetString(bytes);

            //Replace spaces
            Text = Regex.Replace(Text, @"\s", "_", RegexOptions.Compiled);

            //Remove invalid chars
            Text = Regex.Replace(Text, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            Text = Text.Trim('-', '_');

            //Replace double occurences of - or _
            Text = Regex.Replace(Text, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return Text;
        }

    }
}
