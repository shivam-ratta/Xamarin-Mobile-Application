using System;
using System.Collections.Generic;
using System.Text;

namespace PocketPro
{
    public static class Extensions
    {
        public static string ToBytesCount(this long bytes, bool isISO = true)
        {
            int unit = 1024;
            string unitStr = "b";
            if (!isISO) unit = 1000;
            if (bytes < unit) return string.Format("{0} {1}", bytes, unitStr);
            else unitStr = unitStr.ToUpper();
            if (isISO) unitStr = "i" + unitStr;
            int exp = (int)(Math.Log(bytes) / Math.Log(unit));
            return string.Format("{0:##.##} {1}{2}", bytes / Math.Pow(unit, exp), "KMGTPEZY"[exp - 1], unitStr);
        }

    }
}
