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

        take = transform.Find("TakeButton").GetComponent<RectTransform>();
        takeAll = transform.Find("TakeAllButton").GetComponent<RectTransform>();
        use = transform.Find("UseButton").GetComponent<RectTransform>();
        remove = transform.Find("RemoveButton").GetComponent<RectTransform>();
        store = transform.Find("StoreButton").GetComponent<RectTransform>();

        inventorySelection = transform.Find("InventorySelectionBorder").GetComponent<RectTransform>();

        playerInventoryToggle = inventorySelection.transform.GetChild(0).transform.Find("PlayerInventory").GetComponent<Toggle>();
        nonPlayerInventoryToggle = inventorySelection.transform.GetChild(0).transform.Find("StorageInventory").GetComponent<Toggle>();

        hidePos = -screenHeight;
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
        use.anchoredPosition = new Vector2(use.anchoredPosition.x, showPos);
    }

    public void HideUseButton()
    {
        use.anchoredPosition = new Vector2(use.anchoredPosition.x, hidePos);
    }

    public void ShowStoreButton()
    {
        store.anchoredPosition = new Vector2(store.anchoredPosition.x, showPos);
    }

    public void HideStoreButton()
    {
        store.anchoredPosition = new Vector2(store.anchoredPosition.x, hidePos);
    }

    public void ShowRemoveButton()
    {
        remove.anchoredPosition = new Vector2(remove.anchoredPosition.x, showPos);
    }

    public void HideRemoveButton()
    {
        remove.anchoredPosition = new Vector2(remove.anchoredPosition.x, hidePos);
    }

    public void ShowTakeButton()
    {
        take.anchoredPosition = new Vector2(take.anchoredPosition.x, showPos);
    }

    public void HideTakeButton()
    {
        take.anchoredPosition = new Vector2(take.anchoredPosition.x, hidePos);
    }

    public void ShowTakeAllButton()
    {
        takeAll.anchoredPosition = new Vector2(takeAll.anchoredPosition.x, showPos);
    }

    public void HideTakeAllButton()
    {
        takeAll.anchoredPosition = new Vector2(takeAll.anchoredPosition.x, hidePos);
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
