using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class Item : Interactable {

    public ItemHolder itemHolder;
    
    [Header("Item")]
    public string itemName;
    public string itemDescription;
    public string itemResource;
    public ActionType actionType;
    public ItemType itemType;
    public EquipmentType equipmentType;

    protected override void PerformInteraction()
    {
        PlayerInventoryHolder.Instance.AddItemToInventory(itemHolder);
        RemoveEventListener();
        Destroy(gameObject);
    }

    protected override void CreateOptions(Vector3 position)
    {
        AddInteractionButton(position, "Take");
    }

    new void Awake()
    {
        GetComponent<MeshCollider>().convex = true;
        if (itemHolder != null)
        {
            SetInfoForInspector();
        }
        else
        {
            itemHolder = new ItemHolder(itemName, itemDescription, itemResource, actionType, itemType, equipmentType);
        }
    }
	
    protected override void Start()
    {
        base.Start();
        interactableType = InteractableType.Pickable;
    }

    public override void Alive(bool isKinematic, ItemHolder item)
    {
        base.Alive(isKinematic, item);
        IsKinematic(isKinematic);
        SaveItemHolderIntoItem(item);
    }

    void IsKinematic(bool isKinematic)
    {
        GetComponent<Rigidbody>().isKinematic = isKinematic;
    }

    void SetInfoForInspector()
    {
        itemName = itemHolder.itemName;
        itemDescription = itemHolder.itemDescription;
        itemResource = itemHolder.itemResource;
        actionType = itemHolder.actionType;
        itemType = itemHolder.itemType;
        equipmentType = itemHolder.equipmentType;
    }

    void SaveItemHolderIntoItem(ItemHolder itemHolder)
    {
        this.itemHolder = itemHolder;
        SetInfoForInspector();
    }

    public override InteractableType GetInteractableType()
    {
        return interactableType;
    }

    

}
