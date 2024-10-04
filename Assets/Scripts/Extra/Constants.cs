using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class Constants
{
    public static string convertBigNumber(string bigNumber)
    {
        return string.Format(CultureInfo.InvariantCulture, "{0:N0}", bigNumber);
    }
}
