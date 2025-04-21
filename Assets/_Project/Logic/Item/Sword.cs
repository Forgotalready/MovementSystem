using UnityEngine;

[CreateAssetMenu(menuName = ("Item/Sword"))]
public class Sword : Item, IEquipable
{
    [field: SerializeField] public GameObject EquipItemModel { get; private set; }

    public override void Use()
    {
        Debug.Log("SwordUse");
    }

    public void Equip(GameObject player)
    {
        Transform handTransform = player.transform.Find("Hand");
        Instantiate(EquipItemModel, handTransform);
    }

    public void Unequip(GameObject player)
    {
        throw new System.NotImplementedException();
    }
}