using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Extensions
{
    
    public static class TimeSpanExtensions
    {
        public static string StringFormatTimeHHMM(this TimeSpan t)
        {
            return String.Format("{0}:{1}", (t.Days * 24 + t.Hours).ToString().PadLeft(2, '0'), t.Minutes.ToString().PadRight(2, '0'));
        }

    }
}
