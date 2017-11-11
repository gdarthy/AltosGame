using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class Item : Interactable {

    ItemHolder itemHolder;

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

    void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshCollider>().convex = true;
    }
	
    public void SaveItemHolderIntoItem(ItemHolder itemHolder)
    {
        this.itemHolder = itemHolder;
    }

}
