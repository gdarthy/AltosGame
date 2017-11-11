using UnityEngine.UI;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{

    public ItemHolder Item { get; set; }

    public void InitItemContainer(ItemHolder item)
    {
        Item = item;
        transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Icons/Items/" + item.itemResource);
        transform.GetChild(1).GetComponent<Text>().text = item.itemName;
    }

    public void OnClick()
    {
        InventoryManager.Instance.OnSelect(this);
    }

}
