using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class ItemContainer : MonoBehaviour
{
    List<Item> containerStack = new List<Item>();
    public int amount { get; private set; }
    public string itemName { get; private set; }

    public Item GetItem()
    {
        if (containerStack.Count > 0)
        {
            return containerStack[containerStack.Count - 1]; ;
        }
        else
        {
            Debug.Log("Nothing in stack!");
            return null;
        }
    }

    public void RemoveItem()
    {
        containerStack.Remove(containerStack[containerStack.Count - 1]);
        amount = containerStack.Count;
        transform.GetChild(2).GetComponent<Text>().text = containerStack.Count == 1 ? "" : containerStack.Count.ToString();
    }

    public void AddToStack(Item item)
    {
        if (!Contains(item))
        {
            InitItemContainer(item);
        }
        else
        {
            containerStack.Add(item);
        }
        amount = containerStack.Count;
        transform.GetChild(2).GetComponent<Text>().text = containerStack.Count == 1 ? "" : containerStack.Count.ToString();
    }

    bool Contains(Item item)
    {
        foreach (Item _item in containerStack)
        {
            if (_item.itemName == item.itemName)
            {
                return true;
            }
        }
        return false;
    }

    void InitItemContainer(Item item)
    {
        itemName = item.itemName;
        transform.GetChild(0).GetComponent<Image>().sprite = item.itemIcon;
        transform.GetChild(1).GetComponent<Text>().text = item.itemName;
        if ((item.itemType == ItemType.Weapons || item.itemType == ItemType.Tools) && ((Equipment)item).isEquipped)
        {
            transform.GetChild(3).GetComponent<Text>().enabled = true;
            transform.GetChild(3).GetComponent<Text>().text = ((Weapon)item).equippedHand ? "R" : "L";
        }
        else
        {
            transform.GetChild(3).GetComponent<Text>().enabled = false;
        }
        containerStack.Add(item);
    }

    public void OnClick()
    {
        InventoryManager.Instance.OnSelect(this);
    }

}
