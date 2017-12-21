using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInventoryHolder : Inventory
{

    [Header("Storage")]
    public int storageCapacity;

    void Start()
    {
        itemList = new List<Item>(storageCapacity);
    }
}
