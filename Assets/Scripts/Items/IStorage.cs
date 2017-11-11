using UnityEngine;
using System.Collections.Generic;

public interface IStorage  {


    List<ItemHolder> GetItemList();
    void AddItemToStorage(ItemHolder item);
    void RemoveItemFromStorage(ItemHolder item);
    void RemoveAllItems();
    string GetStorageName();
}
