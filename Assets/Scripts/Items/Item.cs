using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    [Header("Item")]
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    public bool isEquipment;

    public string oldId { set; get; }

}

public enum ItemType
{
    Weapons,
    Armor,
    Tools,
    Potions,
    Books,
    Goods
}


