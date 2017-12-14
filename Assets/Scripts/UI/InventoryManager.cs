using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour, IPointerClickHandler
{

    public static InventoryManager Instance { get; private set; }

    InventoryGUIManager inventoryGUIManager;
    IStorage nonPlayerInventory;

    List<ItemContainer> itemContainers = new List<ItemContainer>();

    Details detailsPanel;
    public Transform toggles;
    ItemContainer selected;
    bool inNonPlayerInventory;
    bool anotherInventory = false;

    bool debugging = false;

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

    public void OpenInventory(IStorage nonPlayerInventory)
    {
        _Debug("OpenInventory(IStorage)");
        this.nonPlayerInventory = nonPlayerInventory;
        UpdateList(nonPlayerInventory.GetItemList());
        inNonPlayerInventory = true;
        anotherInventory = true;
        inventoryGUIManager.InventorySelectionPanel(false);
        inventoryGUIManager.ShowInventory();
    }

    public void OpenInventory()
    {
        _Debug("OpenInventory()");
        UpdateList(PlayerInventoryHolder.Instance.GetItemList());
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
        inventoryGUIManager.HideTakeAllButton();
        UpdateList(PlayerInventoryHolder.Instance.GetItemList());
    }

    public void NonPlayerInventory()
    {
        _Debug("NonPlayerInventory()");
        inNonPlayerInventory = true;
        UpdateList(nonPlayerInventory.GetItemList());
        inventoryGUIManager.HideAllButtons();
        inventoryGUIManager.ShowTakeAllButton();
    }

    void SetToggles(List<ItemHolder> itemList)
    {
        _Debug("SetToggles(List<ItemHolder>)");
        foreach (Transform item in toggles)
        {
            item.GetComponent<Toggle>().isOn = false;
        }
        foreach (ItemHolder item in itemList)
        {
            toggles.Find(item.itemType.ToString()).GetComponent<Toggle>().isOn = true;
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

    void CreateInventoryItemList(List<ItemHolder> itemList)
    {
        _Debug("CreateInventoryItemList(List<ItemHolder>)");
        if (itemList.Count > 0)
        {
            _Debug("-ItemList count " + itemList.Count);
            foreach (ItemHolder item in itemList)
            {
                if (item != null)
                {
                    AddItem(item);
                }
                else
                {
                    Debug.Log("--Item is null");
                    break;
                }
            }
        }
        else
        {
            _Debug("-Nothing in ItemList");
        }

    }

    void AddItem(ItemHolder item)
    {
        _Debug("AddItem(ItemHolder)");
        ItemContainer temp = null;
        ItemContainer itemContainer = Resources.Load<ItemContainer>("UI/Item");
        if (itemContainer != null || item != null)
        {
            temp = Instantiate(itemContainer, GameObject.Find(item.itemType + "Panel").transform);
            temp.InitItemContainer(item);
            itemContainers.Add(temp);
        }
        else
        {
            Debug.Log("Something is null" + temp + "or " + item);
        }
    }

    public void OnSelect(ItemContainer itemContainer)
    {
        selected = itemContainer;
        ShowDetails(selected.Item);
        if (inNonPlayerInventory)
        {
            inventoryGUIManager.ShowTakeButton();
        }
        else
        {
            if (anotherInventory)
                inventoryGUIManager.ShowStoreButton();
            inventoryGUIManager.ShowUseButton();
            inventoryGUIManager.ShowRemoveButton();
        }
    }

    public void OnDeselect()
    {
        selected = null;
        inventoryGUIManager.HideAllButtons();
        if (!inNonPlayerInventory || itemContainers.Count == 0)
        {
            inventoryGUIManager.HideTakeAllButton();
        }
        detailsPanel.SetDefault();
    }

    void ShowDetails(ItemHolder item)
    {
        inventoryGUIManager.SetUseAction(item.actionType.ToString());
        detailsPanel.SetDetails(item.itemName, item.itemDescription, "");
    }

    public void Take()
    {
        if (selected != null)
        {
            PlayerInventoryHolder.Instance.AddItemToInventory(selected.Item);
            nonPlayerInventory.RemoveItemFromStorage(selected.Item);
            UpdateList(nonPlayerInventory.GetItemList());
        }
    }

    public void TakeAll()
    {
        foreach (ItemHolder item in nonPlayerInventory.GetItemList())
        {
            PlayerInventoryHolder.Instance.AddItemToInventory(item);
        }
        inventoryGUIManager.HideTakeAllButton();
        nonPlayerInventory.RemoveAllItems();
        UpdateList(nonPlayerInventory.GetItemList());
    }

    public void Use()
    {
        switch (selected.Item.actionType)
        {
            case ActionType.Eat:
                break;
            case ActionType.Equip:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().Equip(selected.Item);
                inventoryGUIManager.SetUseAction(selected.Item.actionType.ToString());
                break;
            case ActionType.Unequip:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().Unequip(selected.Item);
                inventoryGUIManager.SetUseAction(selected.Item.actionType.ToString());
                break;
            case ActionType.Read:
                break;
            case ActionType.Drink:
                break;
            default:
                break;
        }
    }

    public void Remove()
    {
        if (selected != null)
        {
            PlayerInventoryHolder.Instance.DropItem(selected.Item);
            UpdateList(PlayerInventoryHolder.Instance.GetItemList());
        }
    }

    public void Store()
    {
        if (selected != null)
        {
            nonPlayerInventory.AddItemToStorage(selected.Item);
            PlayerInventoryHolder.Instance.RemoveItemFromInventory(selected.Item);
            UpdateList(PlayerInventoryHolder.Instance.GetItemList());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnDeselect();
    }

    void UpdateList(List<ItemHolder> itemList)
    {
        _Debug("UpdateList(List<ItemHolder>)");
        ClearInventoryItemList();
        SetToggles(itemList);
        CreateInventoryItemList(itemList);
        OnDeselect();
    }

}
