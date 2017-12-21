using UnityEngine.UI;
using UnityEngine;

public class ItemsStorage : Interactable
{
    [Header("ItemsStorage")]
    public InventoryManager inventory;

    protected override void Awake()
    {
        base.Awake();
        requiredTool = ToolType.None;
    }

    public override void Interact(GameObject character)
    {
        base.Interact(character);
        inventory.OpenInventory(GetComponent<Inventory>());
        //RemoveEventListener();
    }

    protected override void CreateOptions()
    {
        AddWalkHereButton();
        AddInteractionButton("Use");
    }
}
