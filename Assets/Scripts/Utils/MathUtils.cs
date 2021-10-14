using UnityEngine;
using System;

public static class MathUtils
{
    public static Vector3 DirectionInEuler(Vector3 direction)
    {
        float rotateDegrees = (float)Math.Atan2(direction.y, direction.x) * (float)(360 / (2 * Math.PI));
        return Vector3.forward * rotateDegrees;
    }
}