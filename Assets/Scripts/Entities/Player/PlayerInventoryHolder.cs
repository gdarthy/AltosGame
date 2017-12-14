using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHolder : MonoBehaviour
{

    public static PlayerInventoryHolder Instance { get; private set; }

    List<ItemHolder> inventoryItems;

    void Awake()
    {
        if (Instance == null)

            //if not, set instance to this
            Instance = this;

        //If instance already exists and it's not this:
        else if (Instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        inventoryItems = new List<ItemHolder>();
    }

    void Start()
    {
        Instance.AddItemToInventory(new ItemHolder("Book of Letter", "A book, where does it come from?", "book_of_letter", ActionType.Read, ItemType.Books));
        Instance.AddItemToInventory(new ItemHolder("Good Apple", "An apple i got from old woman with long nose and big wart.", "good_apple", ActionType.Eat, ItemType.Goods));
        //Instance.AddItemToInventory(new ItemHolder("Pickaxe", "Tool for mining", "pickaxe", ActionType.Equip, ItemType.Tools, EquipmentType.HandRight));
    }

    public List<ItemHolder> GetItemList()
    {
        return inventoryItems;
    }

    public void AddItemToInventory(ItemHolder item)
    {
        inventoryItems.Add(item);
    }

    public void RemoveItemFromInventory(ItemHolder item)
    {
        if (ItemHolder.IsEquipable(item) && GetComponent<Equipment>().IsEquipped(item))
            GetComponent<Equipment>().Unequip(item);
        inventoryItems.Remove(item);
    }

    public void DropItem(ItemHolder item)
    {
        if (ItemHolder.IsEquipable(item))
        {
            GameObject _item = BasicObject.GetObjectFromResources(item);
            if (_item != null)
            {
                Transform headPivot = GameObject.Find("HeadPivot").transform;
                GameObject _object = Instantiate(_item, headPivot.position + (headPivot.forward * 1.3f), new Quaternion(0, 0, 0, 0));
                _object.GetComponent<Item>().Alive(false, item);
                RemoveItemFromInventory(item);
            }
            else
            {
                Debug.Log("No object from resources");
            }
        }
    }
}
