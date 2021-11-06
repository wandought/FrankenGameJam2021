using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoordinateCalculator
{
    public static int CoordinateToInt(Vector2 coordinate)
    {
        return CoordinateToInt(coordinate.x, coordinate.y);
    }

    public static int CoordinateToInt(float x, float y)
    {
        int a = Mathf.RoundToInt(x);
        int b = Mathf.RoundToInt(y);

        return CoordinateToInt(a, b);
    }

    public static int CoordinateToInt(int x, int y)
    {
        x *= 2;
        y *= 2;

        if (x < 0)
            x -= 1;

        if (y < 0)
            y -= 1;

        return 1000 * x + y;
    }
}
