using Godot;
using System;
using System.Collections.Generic;

public static class ExtensionMethods
{
    public static float NextFloat(this Random random, Vector2 range)
    {
        return random.NextFloat(range.x, range.y);
    }

    public static float NextFloat(this Random random, float minValue, float maxValue)
    {
        return (float)(random.NextDouble() * (maxValue - minValue) + minValue);
    }

    public static float Percent(this Timer timer)
    {
        return (float)(1 - timer.TimeLeft / timer.WaitTime);
    }
}
