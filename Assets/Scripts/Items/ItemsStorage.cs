﻿using UnityEngine.UI;
using UnityEngine;

public class ItemsStorage : Interactable
{
    [Header("ItemsStorage")]
    public InventoryManager inventory;

    protected override void Start()
    {
        base.Start();
        interactableType = InteractableType.Lootable;
    }

    public override InteractableType GetInteractableType()
    {
        return interactableType;
    }

    protected override void PerformInteraction()
    {
        inventory.OpenInventory(GetComponent<IStorage>());
        RemoveEventListener();
    }

    protected override void CreateOptions(Vector3 position)
    {
        AddWalkHereButton(position);
        AddInteractionButton(position, "Use");
    }
}
