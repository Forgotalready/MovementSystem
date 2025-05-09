﻿using UnityEngine;
using Zenject;

public class PickupComponent : MonoBehaviour
{
    private Inventory _inventory;
    
    [Inject]
    private void Construct(Inventory inventory) => 
            _inventory = inventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ItemPickup item))
        {
            Item itemData = item.ItemData;
            _inventory.AddItem(itemData);
        }
    }
}