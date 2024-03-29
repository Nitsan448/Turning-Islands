using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PortalColors
{
    public static Color Red = new Color(1, 0, 0, 1);
    public static Color Blue = new Color(0, 0, 1.5f, 1);
    public static Color Green = new Color(0, 1, 0, 1);
    public static Color Purple = Color.magenta;
    public static Color Orange = Color.yellow;

    public static Dictionary<int, Color> ColorByIndex = new()
    {
        { 0, Red },
        { 1, Blue },
        { 2, Green },
        { 3, Purple },
        { 4, Orange },
    };
}