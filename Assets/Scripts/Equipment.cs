using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{

    List<ItemHolder> equipment = new List<ItemHolder>();

    public void Equip(ItemHolder item)
    {
        Debug.Log(item.itemResource + " : " + item.equipmentType.ToString());
        GameObject itemObject = Resources.Load<GameObject>("Objects/Items/" + item.itemType + "/" + item.itemResource);
        if (itemObject != null)
            Instantiate(itemObject, GameObject.Find(item.equipmentType.ToString()).transform);
        else
            Debug.Log("Equipment: Item not found");
    }

    public void Unequip()
    {

    }
}

public enum EquipmentType
{
    Head,
    Chest,
    Legs,
    Feet,
    HandLeft,
    HandRight,
    ArmLeft,
    ArmRight
}