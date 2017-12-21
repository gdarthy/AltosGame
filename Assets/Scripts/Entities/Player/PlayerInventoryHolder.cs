using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHolder : Inventory
{

    public static PlayerInventoryHolder Instance { get; private set; }

    void Awake()
    {
        //Singelton
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        itemList = new List<Item>();
    }

    public void RemoveUnequipped()
    {
        foreach (Item item in itemList.ToArray())
        {
            if (item.isEquipment)
            {
                if (!((Equipment)item).isEquipped) RemoveItem(item);
            }else
            {
                RemoveItem(item);
            }
        }
    }

    public void RemoveFromStack(Item item)
    {
        itemList.Remove(item);
        item.itemName += Empty((int)((Equipment)item).equipmentType);
        AddItem(item);
    }

    public void AddToStack(Item item)
    {
        if (((Equipment)item).isEquipped)
        {
            foreach (Item _item in itemList.ToArray())
            {
                if (_item.itemName == item.itemName)
                {
                    itemList.Remove(_item);
                }
            }
            item.itemName = item.itemName.Substring(0, item.itemName.Length - Empty((int)((Equipment)item).equipmentType).Length);
            AddItem(item);
        }
        else
        {
            Debug.Log("No such item");
        }
    }

    string Empty(int index)
    {
        string empty = "";
        for (int i = 0; i < index; i++)
        {
            empty += " ";
        }
        return empty;
    }
}
