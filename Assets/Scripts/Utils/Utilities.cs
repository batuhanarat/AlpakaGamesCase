using System;
using UnityEngine;

public static class Utilities
{
    public static Transform PlayerTransform;
    public static Color GetColorFromString(string color)
    {
        float red = Hex_to_Dec01(color.Substring(0,2));
        float green = Hex_to_Dec01(color.Substring(2,2));
        float blue = Hex_to_Dec01(color.Substring(4,2));
        float alpha = 1f;
        if (color.Length >= 8) {
            alpha = Hex_to_Dec01(color.Substring(6,2));
        }
        return new Color(red, green, blue, alpha);
    }
    public static float Hex_to_Dec01(string hex)
    {
        return Hex_to_Dec(hex)/255f;
    }

    public static int Hex_to_Dec(string hex)
    {
        return Convert.ToInt32(hex, 16);
    }

    public static void SetPlayerTransform(Transform transform ) {
        PlayerTransform = transform;
    }
}