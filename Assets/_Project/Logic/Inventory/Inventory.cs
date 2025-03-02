using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory
{
    private readonly List<Item> _items = new();

    public event Action<IEnumerable> InventoryChange;
    // считается, что класс не может отдавать своё состояние по прямой ссылке, отдаём по интерфейсу, только для чтения

    public void AddItem(Item item)
    {
        _items.Add(item);
        InventoryChange?.Invoke(_items);
    }

    public void DeleteItem(Item item)
    {
        _items.Remove(item);
        InventoryChange?.Invoke(_items);
    }
}