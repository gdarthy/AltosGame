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
        inventoryItems.Remove(item);
    }
}
