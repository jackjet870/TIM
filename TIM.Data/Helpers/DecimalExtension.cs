using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIM.Data.Helpers
{
    public static class DecimalExtension
    {
        public static string ToStringIgnoreFraction(this decimal foo)
        {
            string result = foo.ToString();
            int indexOfDot = result.IndexOf('.');
            if (indexOfDot > 0)
                result = result.Remove(indexOfDot);

            int indexOfComma = result.IndexOf(',');
            if (indexOfComma > 0)
                result = result.Remove(indexOfComma);

            return result;
        }
    }
}