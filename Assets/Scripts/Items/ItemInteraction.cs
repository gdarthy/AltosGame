using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : Interactable {

    public Item itemHolder;

    int instanceId {get; set;}

    protected new void Awake()
    {
        base.Awake();
        instanceId = GetInstanceID();
        requiredTool = ToolType.None;
    }

    public override void Interact(GameObject character)
    {
        Debug.Log("Interacting");
        /*GameObject referenceHolder = Instantiate(gameObject, GameObject.Find("References").transform);
        referenceHolder.GetComponent<Item>().oldId = instanceId.ToString();
        referenceHolder.GetComponent<MeshCollider>().enabled = false;
        referenceHolder.GetComponent<MeshRenderer>().enabled = false;
        referenceHolder.GetComponent<ItemPhysics>().enabled = false;
        referenceHolder.GetComponent<Rigidbody>().isKinematic = true;
        referenceHolder.transform.name = transform.name;
        referenceHolder.transform.position = referenceHolder.transform.parent.position;*/
        PlayerInventoryHolder.Instance.AddItem(itemHolder);
        Destroy(gameObject);
    }

    protected override void CreateOptions()
    {
        AddInteractionButton("Take");
    }
}

