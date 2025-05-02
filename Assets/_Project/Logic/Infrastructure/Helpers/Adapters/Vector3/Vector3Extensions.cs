using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3Adapter ToAdapter(this Vector3 adaptedObject) =>
            new(adaptedObject);
}