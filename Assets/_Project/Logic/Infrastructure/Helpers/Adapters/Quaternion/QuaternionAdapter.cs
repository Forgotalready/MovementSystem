using System;
using UnityEngine;


[Serializable]
public struct QuaternionAdapter
{
    public float EulerX;
    public float EulerY;
    public float EulerZ;

    public QuaternionAdapter(Quaternion adaptedObject)
    {
        EulerX = adaptedObject.eulerAngles.x;
        EulerY = adaptedObject.eulerAngles.y;
        EulerZ = adaptedObject.eulerAngles.z;
    }
    
    public Quaternion ToUnity() => 
            Quaternion.Euler(EulerX, EulerY, EulerZ);
}