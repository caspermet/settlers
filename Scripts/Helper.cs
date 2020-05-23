using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    internal static bool IsOdd(int value)
    {
        return value % 2 != 0;
    }

    internal static Vector3 ShiftObject(Vector3 position, Vector2 mapSize)
    {
        float xLocal = position.x - (mapSize.x - 1) / 2.0f + 0.5f;
        float zLocal = position.z - (CalculZCoor((int)(mapSize.y - 1)) - 0.25f * CalculZCoor((int)mapSize.y - 1)) / 2;

        return new Vector3(xLocal, position.y, zLocal);
    }

    private static float CalculZCoor(int y)
    {
        return y * 2 / (float)Math.Sqrt(3);
    }
}
