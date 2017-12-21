using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class ItemPhysics : MonoBehaviour
{

    float groundLevel;
    public LayerMask walkable;
    RaycastHit hit;

    public void Awake()
    {
        GetComponent<MeshCollider>().convex = true;
        GetComponent<MeshCollider>().inflateMesh = true;
        GetComponent<MeshCollider>().skinWidth = 0.03f;
    }

    public void Alive(bool isKinematic)
    {
        GetComponent<Rigidbody>().isKinematic = isKinematic;
    }

}
