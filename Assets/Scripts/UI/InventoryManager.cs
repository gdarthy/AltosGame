using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryManager : MonoBehaviour, IPointerClickHandler
{

    public static InventoryManager Instance { get; private set; }

    InventoryGUIManager inventoryGUIManager;
    Inventory nonPlayerInventory;

    List<ItemContainer> itemContainers = new List<ItemContainer>();

    Details detailsPanel;
    public Toggles toggles;
    ItemContainer itemContainer;
    bool inNonPlayerInventory;
    bool anotherInventory = false;
    bool debugging = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);

        inventoryGUIManager = GetComponent<InventoryGUIManager>();
        inventoryGUIManager.HideInventory();
    }

    void _Debug(object message)
    {
        if (debugging)
        {
            Debug.Log(message);
        }
    }

    void Start()
    {
        detailsPanel = GameObject.Find("Details").GetComponent<Details>();
    }

    public void OpenInventory(Inventory nonPlayerInventory)
    {
        _Debug("OpenInventory(IStorage)");
        this.nonPlayerInventory = nonPlayerInventory;
        UpdateList(nonPlayerInventory, true);
        if (nonPlayerInventory.GetItemList().Count > 0)
        {
            inventoryGUIManager.EnableButton(InventoryButton.TakeEverything, true);
        }
        inNonPlayerInventory = true;
        anotherInventory = true;
        inventoryGUIManager.InventorySelectionPanel(false);
        inventoryGUIManager.ShowInventory();
    }

    public void OpenInventory()
    {
        _Debug("OpenInventory()");
        UpdateList(PlayerInventoryHolder.Instance, true);
        inNonPlayerInventory = false;
        anotherInventory = false;
        inventoryGUIManager.InventorySelectionPanel(true);
        inventoryGUIManager.ShowInventory();
    }

    public void PlayerInventory()
    {
        _Debug("PlayerInventory()");
        inNonPlayerInventory = false;
        inventoryGUIManager.HideAllButtons();
        inventoryGUIManager.EnableButton(InventoryButton.TakeEverything, false);
        UpdateList(PlayerInventoryHolder.Instance, true);
        if (PlayerInventoryHolder.Instance.GetItemList().Count > 0)
        {
            inventoryGUIManager.EnableButton(InventoryButton.StoreEverything, true);
        }
    }

    public void NonPlayerInventory()
    {
        _Debug("NonPlayerInventory()");
        inNonPlayerInventory = true;
        UpdateList(nonPlayerInventory, true);
        inventoryGUIManager.HideAllButtons();
        inventoryGUIManager.EnableButton(InventoryButton.TakeEverything, false);
        if (nonPlayerInventory.GetItemList().Count > 0)
        {
            inventoryGUIManager.EnableButton(InventoryButton.TakeEverything, true);
        }
    }

    void SetToggles(Inventory inventory)
    {
        _Debug("SetToggles(List<ItemHolder>)");

        toggles.ResetActive();
        if (!inNonPlayerInventory)
        {
            inventoryGUIManager.SetTogglesMask(false);
            toggles.SetFavoriteToggles();
        }
        else
        {
            inventoryGUIManager.SetTogglesMask(true);
            toggles.ResetFavoriteToggles();
            foreach (Item item in inventory.GetItemList())
            {
                if (item != null)
                    toggles.SetActiveToggle(inventory.GetItemInstance(item).itemType.ToString());
                else Debug.Log("Item null");
            }
        }

    }

    void ClearInventoryItemList()
    {
        _Debug("ClearInventoryItemList()");
        foreach (ItemContainer item in itemContainers)
        {
            Destroy(item.gameObject);
        }
        itemContainers.Clear();
    }

    void CreateInventoryItemList(Inventory inventory)
    {
        List<Item> itemList = inventory.GetItemList();
        if (itemList.Count > 0)
        {
            foreach (Item item in itemList)
            {
                ItemContainer temp = GetItemContainer(item.itemName);
                if (temp == null)
                {
                    ItemContainer itemContainer = Instantiate(Resources.Load<ItemContainer>("UI/Item"), inventoryGUIManager.GetPanel(item.itemType));
                    itemContainer.AddToStack(item);
                    itemContainers.Add(itemContainer);
                }else
                {
                    temp.AddToStack(item);
                    itemContainers.Add(temp);
                }
            }
        }
        else
        {
            _Debug("-Nothing in ItemList");
        }

    }

    ItemContainer GetItemContainer(string name)
    {
        foreach (ItemContainer ic in itemContainers)
        {
            if (ic.itemName == name)
            {
                return ic;
            }
        }
        return null;
    }

    public void OnSelect(ItemContainer itemContainer)
    {
        Item item = itemContainer.GetItem();
        ShowDetails(item);
        if (inNonPlayerInventory)
        {
            inventoryGUIManager.EnableButton(InventoryButton.Take, true);
            inventoryGUIManager.EnableButton(InventoryButton.TakeAll, true);
        }
        else
        {
            inventoryGUIManager.HideAllButtons();
            if (anotherInventory)
            {
                inventoryGUIManager.EnableButton(InventoryButton.Store, true);
                if (itemContainer.amount > 1)
                {
                    inventoryGUIManager.EnableButton(InventoryButton.StoreAll, true);
                }
            }
            else
            {
                if (itemContainer.amount > 1)
                    inventoryGUIManager.EnableButton(InventoryButton.RemoveAll, true);
                else
                    inventoryGUIManager.EnableButton(InventoryButton.RemoveAll, false);
                inventoryGUIManager.EnableButton(InventoryButton.Remove, true);
                if (item.itemType == ItemType.Weapons || item.itemType == ItemType.Tools || item.itemType == ItemType.Armor)
                {
                    bool isEquipped = ((Equipment)item).isEquipped;
                    if (isEquipped)
                    {
                        inventoryGUIManager.EnableButton(InventoryButton.Equip, !isEquipped);
                        inventoryGUIManager.EnableButton(InventoryButton.Unequip, isEquipped);
                    }
                    else
                    {
                        inventoryGUIManager.EnableButton(InventoryButton.Equip, !isEquipped);
                        inventoryGUIManager.EnableButton(InventoryButton.Unequip, isEquipped);
                    }
                }
            }
        }
    }

    public void OnDeselect()
    {
        itemContainer = null;
        inventoryGUIManager.HideAllButtons();
        if (!inNonPlayerInventory || itemContainers.Count == 0)
        {
            inventoryGUIManager.EnableButton(InventoryButton.TakeEverything, false);
        }
        detailsPanel.SetDefault();
    }

    void ShowDetails(Item item)
    {
        detailsPanel.SetDetails(item.itemName, item.itemDescription, "");
    }

    public void Take()
    {
        if (itemContainer != null)
        {
            PlayerInventoryHolder.Instance.AddItem(itemContainer.GetItem());
            nonPlayerInventory.RemoveItem(itemContainer.GetItem());
            UpdateList(nonPlayerInventory, true);
        }
    }

    public void TakeAll()
    {
        if (itemContainer != null)
        {
            PlayerInventoryHolder.Instance.AddWholeStack(itemContainer.GetItem(), nonPlayerInventory.GetItemList());
            nonPlayerInventory.RemoveWholeStack(itemContainer.GetItem());
            UpdateList(nonPlayerInventory, true);
        }
    }

    public void TakeEverything()
    {
        foreach (Item item in nonPlayerInventory.GetItemList())
        {
            PlayerInventoryHolder.Instance.AddWholeStack(nonPlayerInventory.GetItemInstance(item), nonPlayerInventory.GetItemList());
        }
        inventoryGUIManager.EnableButton(InventoryButton.TakeEverything, false);
        nonPlayerInventory.RemoveAllItems();
        UpdateList(nonPlayerInventory, true);
    }

    public void Use()
    {

    }

    // TODO Update Equipping
    public void Equip(bool hand)
    {
        PlayerEquipment.Instance.Equip((Weapon)itemContainer.GetItem(), (hand ? EquipmentType.RightHand : EquipmentType.LeftHand));
        UpdateList(PlayerInventoryHolder.Instance, true);
    }

    public void Unequip()
    {
        PlayerEquipment.Instance.Unequip((Equipment)itemContainer.GetItem());
        UpdateList(PlayerInventoryHolder.Instance, true);
    }

    public void Remove()
    {
        if (itemContainer != null)
        {
            PlayerInventoryHolder.Instance.DropItem(itemContainer.GetItem());
            UpdateList(PlayerInventoryHolder.Instance, true);
        }
    }

    public void RemoveAll()
    {
        if (itemContainer != null)
        {
            PlayerInventoryHolder.Instance.DropAllItems(itemContainer.GetItem());
            UpdateList(PlayerInventoryHolder.Instance, true);
        }
    }

    public void Store()
    {
        if (itemContainer != null)
        {
            PlayerEquipment.Instance.UnequipIfNecessary(itemContainer.GetItem());
            nonPlayerInventory.AddItem(itemContainer.GetItem());
            PlayerInventoryHolder.Instance.RemoveItem(itemContainer.GetItem());
            UpdateList(PlayerInventoryHolder.Instance, true);
        }
    }

    public void StoreAll()
    {
        if (itemContainer != null)
        {
            nonPlayerInventory.AddWholeStack(itemContainer.GetItem(), PlayerInventoryHolder.Instance.GetItemList());
            PlayerInventoryHolder.Instance.RemoveWholeStack(itemContainer.GetItem());
            UpdateList(PlayerInventoryHolder.Instance, true);
        }
    }

    public void StoreEverything()
    {
        foreach (Item item in PlayerInventoryHolder.Instance.GetItemList())
        {
            if (!PlayerEquipment.Instance.IsEquipped(PlayerInventoryHolder.Instance.GetItemInstance(item)))
                nonPlayerInventory.AddWholeStack(PlayerInventoryHolder.Instance.GetItemInstance(item), PlayerInventoryHolder.Instance.GetItemList());
        }
        PlayerInventoryHolder.Instance.RemoveUnequipped(); // TODO removes items that are equipped 
        UpdateList(PlayerInventoryHolder.Instance, true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnDeselect();
    }

    void UpdateList(Inventory inventory, bool deselect)
    {
        ClearInventoryItemList();
        SetToggles(inventory);
        CreateInventoryItemList(inventory);
        OnDeselect();
    }

    void Update()
    {
        if (inventoryGUIManager.IsShown() && !inNonPlayerInventory && itemContainer != null &&
            (itemContainer.GetItem().itemType == ItemType.Weapons || itemContainer.GetItem().itemType == ItemType.Tools))
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Equip(false); // false for left hand
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Equip(true); // true for right hand
            }

        }
    }

}
