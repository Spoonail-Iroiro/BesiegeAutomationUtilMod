using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomationUtil
{
    public class Util
    {
        public static long MMod(long a, long b)
        {
            if (a < 0)
            {
                var n = (-a) / b;
                a = a + b * (n + 1);
            }

            return a % b;
        }
    }
}
