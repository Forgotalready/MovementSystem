using UnityEngine;

[CreateAssetMenu(menuName = ("Item/Sword"))]
public class Sword : Item
{
    public override void Use()
    {
        Debug.Log("SwordUse");
    }
}