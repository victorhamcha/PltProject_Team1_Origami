using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Tools
{

    public enum OverwriteType
    {
        X, Y, Z
    }

    public static Vector2 To2D(this Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }

    public static Vector3 To3D(this Vector2 position)
    {
        return position.To3D(0f);
    }

    public static Vector3 To3D(this Vector2 position, float value)
    {
        return new Vector3(position.x, value, position.y);
    }

    public static void SubstractAboveZero(this ref Vector2 vector, Vector2 value)
    {
        if (vector.sqrMagnitude >= value.sqrMagnitude)
        {
            vector -= value;
        }
        else
        {
            vector = Vector2.zero;
        }
    }

    public static Vector3 Overwrite(this Vector3 vector, OverwriteType type, float value = 0f)
    {
        switch (type)
        {
            case OverwriteType.X:
                return new Vector3(value, vector.y, vector.z);
            case OverwriteType.Y:
                return new Vector3(vector.x, value, vector.z);
            case OverwriteType.Z:
                return new Vector3(vector.x, vector.y, value);
        }
        return vector;
    }
}