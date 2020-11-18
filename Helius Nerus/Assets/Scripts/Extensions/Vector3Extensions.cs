﻿using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        Vector3 result = vector;
        if (x != null)
            result.x = x.Value;
        if (y != null)
            result.y = y.Value;
        if (z != null)
            result.z = z.Value;
        return result;
    }

    public static Vector2 ClampInBorders(this Vector2 vector, float border)
    {
        float length = vector.magnitude;
        if (length > border)
        {
            float delta = length / border;
            vector.x /= delta;
            vector.y /= delta;
        }
        return vector;
    }
}
