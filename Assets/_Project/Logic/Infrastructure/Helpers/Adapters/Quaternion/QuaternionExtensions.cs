using UnityEngine;

public static class QuaternionExtensions
{
    public static QuaternionAdapter ToAdapter(this Quaternion adaptedObject) =>
            new(adaptedObject);
}