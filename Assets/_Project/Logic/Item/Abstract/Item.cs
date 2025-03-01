using System;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [field: SerializeField] public String Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public String Description { get; private set; }

    public abstract void Use();
}