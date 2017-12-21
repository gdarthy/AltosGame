using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public interface IStorage  {


    Hashtable GetItemList();
    void AddItemToStorage(Item item);
    void RemoveItemFromStorage(Item item);
    void RemoveAllItems();
    string GetStorageName();
}
