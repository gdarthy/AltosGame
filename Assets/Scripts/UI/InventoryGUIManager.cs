using UnityEngine;
using UnityEngine.UI;

public class InventoryGUIManager : MonoBehaviour
{

    RectTransform take;
    RectTransform takeAll;
    RectTransform use;
    RectTransform remove;
    RectTransform store;
    RectTransform inventorySelection;
    Toggle playerInventoryToggle;
    Toggle nonPlayerInventoryToggle;

    int screenHeight;
    float buttonTopMargin = -10f;
    float hidePos;
    float showPos;

    void Start()
    {
        screenHeight = Screen.height;

        take = GameObject.Find("TakeButton").GetComponent<RectTransform>();
        takeAll = GameObject.Find("TakeAllButton").GetComponent<RectTransform>();
        use = GameObject.Find("UseButton").GetComponent<RectTransform>();
        remove = GameObject.Find("RemoveButton").GetComponent<RectTransform>();
        store = GameObject.Find("StoreButton").GetComponent<RectTransform>();

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
            HideRemoveButton();
            HideUseButton();
        }
    }

    public void HideAllButtons()
    {
        HideTakeButton();
        HideRemoveButton();
        HideUseButton();
        HideStoreButton();
    }

    public void SetUseAction(string useAction)
    {
        use.GetComponentInChildren<Text>().text = useAction;
    }

    public void ShowUseButton()
    {
        use.GetComponent<LayoutElement>().ignoreLayout = false;
        use.pivot = new Vector2(use.pivot.x, showPos);
    }

    public void HideUseButton()
    {
        use.GetComponent<LayoutElement>().ignoreLayout = true;
        use.pivot = new Vector2(use.pivot.x, hidePos);
    }

    public void ShowStoreButton()
    {
        store.GetComponent<LayoutElement>().ignoreLayout = false;
        store.pivot = new Vector2(store.pivot.x, showPos);
    }

    public void HideStoreButton()
    {
        store.GetComponent<LayoutElement>().ignoreLayout = true;
        store.pivot = new Vector2(store.pivot.x, hidePos);
    }

    public void ShowRemoveButton()
    {
        remove.GetComponent<LayoutElement>().ignoreLayout = false;
        remove.pivot = new Vector2(remove.pivot.x, showPos);
    }

    public void HideRemoveButton()
    {
        remove.GetComponent<LayoutElement>().ignoreLayout = true;
        remove.pivot = new Vector2(remove.pivot.x, hidePos);
    }

    public void ShowTakeButton()
    {
        take.GetComponent<LayoutElement>().ignoreLayout = false;
        take.pivot = new Vector2(take.pivot.x, showPos);
    }

    public void HideTakeButton()
    {
        take.GetComponent<LayoutElement>().ignoreLayout = true;
        take.pivot = new Vector2(take.pivot.x, hidePos);
    }

    public void ShowTakeAllButton()
    {
        takeAll.GetComponent<LayoutElement>().ignoreLayout = false;
        takeAll.pivot = new Vector2(takeAll.pivot.x, showPos);
    }

    public void HideTakeAllButton()
    {
        takeAll.GetComponent<LayoutElement>().ignoreLayout = true;
        takeAll.pivot = new Vector2(takeAll.pivot.x, hidePos);
    }

    public void ShowInventory()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.localPosition = new Vector3(rt.localPosition.x, screenHeight / 2 - transform.parent.GetComponent<RectTransform>().localPosition.y, rt.localPosition.z);
    }

    public void HideInventory()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.localPosition = new Vector3(rt.localPosition.x, -transform.parent.GetComponent<RectTransform>().localPosition.y * 2, rt.localPosition.z);
    }
}
