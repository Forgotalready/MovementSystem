using UnityEngine;
using System;

[Serializable]
public struct Vector3Adapter
{
    public float X;
    public float Y;
    public float Z;

    public Vector3Adapter(Vector3 adaptedObject)
    {
        X = adaptedObject.x;
        Y = adaptedObject.y;
        Z = adaptedObject.z;
    }

    public Vector3 ToUnity() =>
            new(X, Y, Z);
}