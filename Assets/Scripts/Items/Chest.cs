using UnityEngine;
using System.Collections.Generic;
using System;

public class Chest : Storage, IStorage
{
    [Header("Chest")]
    public string chestName;
    public List<ItemHolder> content;

    void Start()
    {
        content = new List<ItemHolder>(storageCapacity);
        ItemHolder item1 = new ItemHolder("Hatchet", "Tool for cutting trees, axe or hatchet?", "hatchet", ActionType.Equip, ItemType.Tools, EquipmentType.HandRight);

        content.Add(item1);

    }

    public void AddItemToStorage(ItemHolder item)
    {
        content.Add(item);
    }

    public List<ItemHolder> GetItemList()
    {
        return content;
    }

    public string GetStorageName()
    {
        return chestName;
    }

    public void RemoveAllItems()
    {
        content.Clear();
    }

    public void RemoveItemFromStorage(ItemHolder item)
    {
        content.Remove(item);
    }
}

