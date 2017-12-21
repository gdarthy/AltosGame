using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    protected List<Item> itemList;

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public void AddWholeStack(Item item, List<Item> itemStack)
    {
        foreach (Item _item in itemStack)
        {
            if (item.itemName == _item.itemName)
            {
                AddItem(_item);
            }
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public Item GetItemInstance(Item item)
    {
        foreach (Item _item in itemList)
        {
            if (item == _item)
            {
                return _item;
            }
        }
        return null;
    }

    public void DropItem(Item item)
    {
        PlayerEquipment.Instance.UnequipIfNecessary(item);
        GameObject _item = item.itemPrefab;
        if (_item != null)
        {
            Transform headPivot = GameObject.Find("HeadPivot").transform;
            GameObject _object = Instantiate(_item, headPivot.position + (headPivot.forward * 1.3f), new Quaternion(0, 0, 0, 0));
            _object.GetComponent<ItemPhysics>().Alive(false);
            //DestroyReference(item.oldId);
            RemoveItem(item);
        }
        else
        {
            Debug.Log("No object from resources");
        }
    }

    public void DropAllItems(Item item)
    {
        // TODO
        GameObject _item;
        foreach (Item listItem in itemList.ToArray())
        {
            if (listItem.itemName == item.itemName)
            {
                _item = listItem.itemPrefab;
                DropItem(item);
            }
        }
    }

    public void RemoveAllItems()
    {
        itemList.Clear();
    }

    public void RemoveWholeStack(Item item)
    {
        foreach (Item _item in itemList.ToArray())
        {
            if (item.itemName == _item.itemName)
            {
                RemoveItem(_item);
            }
        }
    }

    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
    }
}
