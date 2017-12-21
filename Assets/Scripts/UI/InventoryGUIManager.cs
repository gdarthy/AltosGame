using UnityEngine;
using UnityEngine.UI;

public class InventoryGUIManager : MonoBehaviour
{

    RectTransform take;
    RectTransform takeAll;
    RectTransform takeEverything;
    RectTransform use;
    RectTransform remove;
    RectTransform removeAll;
    RectTransform store;
    RectTransform storeAll;
    RectTransform storeEverything;
    RectTransform equip;
    RectTransform unequip;
    RectTransform inventorySelection;
    Toggle playerInventoryToggle;
    Toggle nonPlayerInventoryToggle;
    Image togglesMask;

    public RectTransform weaponsPanel;
    public RectTransform toolsPanel;
    public RectTransform armorPanel;
    public RectTransform goodsPanel;
    public RectTransform booksPanel;
    public RectTransform potionsPanel;
    public RectTransform favoritesPanel;

    int screenHeight;
    float buttonTopMargin = -10f;
    float hidePos;
    float showPos;
    bool isShown = false;

    void Start()
    {
        screenHeight = Screen.height;

        take = GameObject.Find("TakeButton").GetComponent<RectTransform>();
        takeAll = GameObject.Find("TakeAllButton").GetComponent<RectTransform>();
        takeEverything = GameObject.Find("TakeEverythingButton").GetComponent<RectTransform>();
        use = GameObject.Find("UseButton").GetComponent<RectTransform>();
        remove = GameObject.Find("RemoveButton").GetComponent<RectTransform>();
        removeAll = GameObject.Find("RemoveAllButton").GetComponent<RectTransform>();
        store = GameObject.Find("StoreButton").GetComponent<RectTransform>();
        storeAll = GameObject.Find("StoreAllButton").GetComponent<RectTransform>();
        storeEverything = GameObject.Find("StoreEverythingButton").GetComponent<RectTransform>();
        equip = GameObject.Find("EquipButton").GetComponent<RectTransform>();
        unequip = GameObject.Find("UnequipButton").GetComponent<RectTransform>();
        togglesMask = GameObject.Find("TogglesMask").GetComponent<Image>();
        inventorySelection = transform.Find("InventorySelectionBorder").GetComponent<RectTransform>();

        playerInventoryToggle = inventorySelection.transform.GetChild(0).transform.Find("PlayerInventory").GetComponent<Toggle>();
        nonPlayerInventoryToggle = inventorySelection.transform.GetChild(0).transform.Find("StorageInventory").GetComponent<Toggle>();

        hidePos = 20;
        showPos = buttonTopMargin;
    }

    public void InventorySelectionPanel(bool onlyPlayer)
    {
        if (onlyPlayer)
        {
            playerInventoryToggle.isOn = true;
            nonPlayerInventoryToggle.isOn = false;
            inventorySelection.offsetMin = new Vector2(inventorySelection.offsetMin.x, 50);
            HideAllButtons();
        }else
        {
            playerInventoryToggle.isOn = false;
            nonPlayerInventoryToggle.isOn = true;
            inventorySelection.offsetMin = new Vector2(inventorySelection.offsetMin.x, 0);
            EnableButton(InventoryButton.Use, false);
            EnableButton(InventoryButton.Remove, false);
        }
    }

    public void SetTogglesMask(bool active)
    {
        togglesMask.raycastTarget = active;
    }
    public void HideAllButtons()
    {
        EnableButton(InventoryButton.Take, false);
        EnableButton(InventoryButton.TakeAll    , false);
        EnableButton(InventoryButton.Use, false);
        EnableButton(InventoryButton.Remove, false);
        EnableButton(InventoryButton.RemoveAll, false);
        EnableButton(InventoryButton.Store, false);
        EnableButton(InventoryButton.StoreAll, false);
        EnableButton(InventoryButton.StoreEverything, false);
        EnableButton(InventoryButton.Equip, false);
        EnableButton(InventoryButton.Unequip, false);
    }

    public Transform GetPanel(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Weapons:
                return weaponsPanel.transform;
            case ItemType.Armor:
                return armorPanel.transform;
            case ItemType.Tools:
                return toolsPanel.transform;
            case ItemType.Potions:
                return potionsPanel.transform;
            case ItemType.Books:
                return booksPanel.transform;
            case ItemType.Goods:
                return goodsPanel.transform;
        }
        return null;
    }

    public void EnableButton(ItemType itemType, bool enable)
    {
        switch (itemType)
        {
            case ItemType.Weapons:
            case ItemType.Armor:
            case ItemType.Tools:
                EnableButton(InventoryButton.Equip, enable);
                break;
            case ItemType.Potions:
                EnableButton(InventoryButton.Drink, enable);
                break;
            case ItemType.Books:
                EnableButton(InventoryButton.Read, enable);
                break;
            case ItemType.Goods:
                EnableButton(InventoryButton.None, enable);
                break;
        }
    }

    public void EnableButton(InventoryButton button, bool enable)
    {
        float position = enable ? showPos : hidePos;
        switch (button)
        {
            case InventoryButton.Take:
                take.GetComponent<LayoutElement>().ignoreLayout = !enable;
                take.pivot = new Vector2(take.pivot.x, position);
                break;
            case InventoryButton.TakeAll:
                takeAll.GetComponent<LayoutElement>().ignoreLayout = !enable;
                takeAll.pivot = new Vector2(takeAll.pivot.x, position);
                break;
            case InventoryButton.TakeEverything:
                takeEverything.GetComponent<LayoutElement>().ignoreLayout = !enable;
                takeEverything.pivot = new Vector2(takeEverything.pivot.x, position);
                break;
            case InventoryButton.Use:
                use.GetComponent<LayoutElement>().ignoreLayout = !enable;
                use.pivot = new Vector2(use.pivot.x, position);
                break;
            case InventoryButton.Store:
                store.GetComponent<LayoutElement>().ignoreLayout = !enable;
                store.pivot = new Vector2(store.pivot.x, position);
                break;
            case InventoryButton.StoreAll:
                storeAll.GetComponent<LayoutElement>().ignoreLayout = !enable;
                storeAll.pivot = new Vector2(storeAll.pivot.x, position);
                break;
            case InventoryButton.StoreEverything:
                storeEverything.GetComponent<LayoutElement>().ignoreLayout = !enable;
                storeEverything.pivot = new Vector2(storeEverything.pivot.x, position);
                break;
            case InventoryButton.Remove:
                remove.GetComponent<LayoutElement>().ignoreLayout = !enable;
                remove.pivot = new Vector2(remove.pivot.x, position);
                break;
            case InventoryButton.RemoveAll:
                removeAll.GetComponent<LayoutElement>().ignoreLayout = !enable;
                removeAll.pivot = new Vector2(removeAll.pivot.x, position);
                break;
            case InventoryButton.Equip:
                equip.GetComponent<LayoutElement>().ignoreLayout = !enable;
                equip.pivot = new Vector2(equip.pivot.x, position);
                break;
            case InventoryButton.Unequip:
                unequip.GetComponent<LayoutElement>().ignoreLayout = !enable;
                unequip.pivot = new Vector2(unequip.pivot.x, position);
                break;
        }
        
    }

    public void ShowInventory()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.localPosition = new Vector3(rt.localPosition.x, screenHeight / 2 - transform.parent.GetComponent<RectTransform>().localPosition.y, rt.localPosition.z);
        isShown = true;
    }

    public void HideInventory()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.localPosition = new Vector3(rt.localPosition.x, -transform.parent.GetComponent<RectTransform>().localPosition.y * 2, rt.localPosition.z);
        isShown = false;
    }

    public bool IsShown()
    {
        return isShown;
    }

}

public enum InventoryButton
{
    Take,
    TakeAll,
    TakeEverything,
    Use,
    Store,
    StoreAll,
    StoreEverything,
    Remove,
    RemoveAll,
    Equip,
    Unequip,
    Drink,
    Read,
    None
}