using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class Chest : StorageInventoryHolder
{
    [Header("Chest")]
    public string chestName;

    public string GetStorageName()
    {
        return chestName;
    }

}

