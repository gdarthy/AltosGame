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

    public Details detailsPanel;
    public Transform toggles;
    ItemContainer selected;
    bool inNonPlayerInventory;
    bool anotherInventory = false;

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

    void Start()
    {
        detailsPanel = GameObject.Find("Details").GetComponent<Details>();
    }

    public void OpenInventory(IStorage nonPlayerInventory)
    {
        this.nonPlayerInventory = nonPlayerInventory;
        UpdateList(nonPlayerInventory.GetItemList());
        inNonPlayerInventory = true;
        anotherInventory = true;
        inventoryGUIManager.InventorySelectionPanel(false);

        inventoryGUIManager.ShowInventory();
    }

    public void OpenInventory()
    {
        UpdateList(PlayerInventoryHolder.Instance.GetItemList());
        inNonPlayerInventory = false;
        anotherInventory = false;
        inventoryGUIManager.InventorySelectionPanel(true);
        inventoryGUIManager.ShowInventory();
    }

    public void PlayerInventory()
    {
        inNonPlayerInventory = false;
        inventoryGUIManager.HideAllButtons();
        inventoryGUIManager.HideTakeAllButton();
        UpdateList(PlayerInventoryHolder.Instance.GetItemList());
    }

    public void NonPlayerInventory()
    {
        inNonPlayerInventory = true;
        UpdateList(nonPlayerInventory.GetItemList());
        inventoryGUIManager.HideAllButtons();
        inventoryGUIManager.ShowTakeAllButton();
    }

    void SetToggles(List<ItemHolder> itemList)
    {
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
        foreach (ItemContainer item in itemContainers)
        {
            Destroy(item.gameObject);
        }
        itemContainers.Clear();
    }

    void CreateInventoryItemList(List<ItemHolder> itemList)
    {
        foreach (ItemHolder item in itemList)
        {
            AddItem(item);
        }
    }

    void AddItem(ItemHolder item)
    {
        ItemContainer temp = null;
        ItemContainer itemContainer = Resources.Load<ItemContainer>("UI/Item");
        temp = Instantiate(itemContainer, GameObject.Find(item.itemType + "Panel").transform);
        temp.InitItemContainer(item);
        itemContainers.Add(temp);
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
            PlayerInventoryHolder.Instance.RemoveItemFromInventory(selected.Item);
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
        ClearInventoryItemList();
        SetToggles(itemList);
        CreateInventoryItemList(itemList);
        OnDeselect();
    }

}
