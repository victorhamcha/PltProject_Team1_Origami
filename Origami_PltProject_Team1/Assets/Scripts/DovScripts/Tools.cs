using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools 
{
    
    public enum OverwriteType
    {
        X, Y, Z
    }

    /*public static Vector3 Overwrite(this Vector3 vector, OverwriteType type, float value = 0f)
    {
        switch (type)
        {
            case OverwriteType.X:
                return new Vector3(value, vector.y, vector.z);
            case OverwriteType.Y:
                return new Vector3(vector.y, value, vector.z);
            case OverwriteType.Z:
                return new Vector3(vector.x, value, value);
        }
    }*/
}
