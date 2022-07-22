using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PortalColors
{
    public static Color Red = Color.red;
    public static Color Blue = Color.blue;
    public static Color Green = Color.green;
    public static Color Purple = Color.magenta;
    public static Color Orange = Color.yellow;

    public static Dictionary<int, Color> ColorByIndex = new Dictionary<int, Color>()
    {
        { 0, Red },
        { 1, Blue },
        { 2, Green },
        { 3, Purple },
        { 4, Orange },
    };
}
