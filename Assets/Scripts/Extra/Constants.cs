using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public enum level
{
    level1,
    level2,
    level3
}
public static class Constants
{

    public static string convertBigNumber(string bigNumber)
    {
        return string.Format(CultureInfo.InvariantCulture, "{0:N0}", bigNumber);
    }
    public static string convertBigNumber(int bigNumber)
    {
        string sbignumber = bigNumber.ToString();
        return string.Format(CultureInfo.InvariantCulture, "{0:N0}", sbignumber);
    }
}
