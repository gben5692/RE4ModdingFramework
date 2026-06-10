using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Logging
{
    internal static class Time
    {
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss:fff");
        }

        public static string GetDay()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
