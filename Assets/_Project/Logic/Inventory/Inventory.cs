using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Inventory
{
    private readonly List<Item> _items = new();
    private GameObject _player;

    public event Action<ReadOnlyCollection<Item>> InventoryChange;
    // считается, что класс не может отдавать своё состояние по прямой ссылке, отдаём по интерфейсу, только для чтения
    public Inventory(GameObject player)
    {
        _player = player;
    }
    
    public void AddItem(Item item)
    {
        _items.Add(item);
        InventoryChange?.Invoke(_items.AsReadOnly());
    }

    public void DeleteItem(Item item)
    {
        _items.Remove(item);
        InventoryChange?.Invoke(_items.AsReadOnly());
    }

    public void Use(int index)
    {
        if (index >= _items.Count)
        {
            Debug.LogError("Индекс выходит за пределы диапазона");
        }

        if (_items[index] is IEquipable equipable)
        {
            equipable.Equip(_player);
        }
        else
        {
            _items[index].Use();
        }
        
        _items.RemoveAt(index);
        InventoryChange?.Invoke(_items.AsReadOnly());
    }
}