using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{

    GameObject[] equipment = new GameObject[(int)EquipmentType.Lenght];

    bool isEquipped;

    public void Equip(ItemHolder item)
    {
        Debug.Log("Equipping: " + item.itemResource + " : " + item.equipmentType.ToString());
        GameObject itemObject = BasicObject.GetObjectFromResources(item);
        int itemTypeInt = (int)item.equipmentType;
        isEquipped = false;

        if (itemObject != null)
        {
            GameObject _item = Instantiate(itemObject, GetEquipmentParent(item));
            _item.GetComponent<Item>().Alive(true, item);

            if (equipment[itemTypeInt] == null)
                equipment[itemTypeInt] = _item;
            else if (equipment[itemTypeInt] == _item)
            {
                isEquipped = true;
            }
            else
                Unequip(equipment[itemTypeInt].GetComponent<Item>().itemHolder);

            if (!isEquipped)
            {
                equipment[itemTypeInt] = _item;

                item.SwitchEquipAction();
            }
            else
            {
                Debug.Log("Equipment: Item already equipped");
            }
        }
        else
            Debug.Log("Equipment: Item not found");
    }

    public void Unequip(ItemHolder item)
    {
        Destroy(GetEquipmentParent(item).transform.GetChild(0).gameObject);
        equipment[(int)item.equipmentType] = null;
        item.SwitchEquipAction();
    }

    public bool IsEquipped(ItemHolder item)
    {
        if (equipment[(int)item.equipmentType] == null)
        {
            return false;
        }
        return equipment[(int)item.equipmentType].GetComponent<Item>().itemHolder == item;
    }

    Transform GetEquipmentParent(ItemHolder item)
    {
        if (item.equipmentType == EquipmentType.None)
        {
            Debug.Log("No parent");
            return null;
        }
        else
            return GameObject.Find(item.equipmentType.ToString()).transform;
    }

    public Item GetEquipedItem(EquipmentType equipmentType)
    {
        if (equipment[(int)equipmentType] != null)
        {
            return equipment[(int)equipmentType].GetComponent<Item>();
        }else
        {
            return null;
        }
    }


}

public enum EquipmentType
{
    None,
    Head,
    Chest,
    Legs,
    Feet,
    HandLeft,
    HandRight,
    ArmLeft,
    ArmRight,
    Lenght
}