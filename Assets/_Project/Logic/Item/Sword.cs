using UnityEngine;

[CreateAssetMenu(menuName = ("Item/Sword"))]
public class Sword : Item, IEquipable
{
    [field: SerializeField] public GameObject equipItemModel;
    
    public override void Use()
    {
        Debug.Log("SwordUse");
    }

    public void Equip(GameObject player)
    {
        Transform handTransform = player.transform.Find("Hand");
        Instantiate(equipItemModel, handTransform);
    }

    public void Unequip(GameObject player)
    {
        throw new System.NotImplementedException();
    }
}