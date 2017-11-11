using UnityEngine;
using System.Collections;

public class Attach : MonoBehaviour
{

    public Transform helmet;
    public Transform target;

    void Start()
    {
        /*foreach (Transform child in transform)
        {
            Iterate(child, "");
        }*/
        //transform.
        //helmet.parent = target;// Attach hat to head bone.
        //helmet.localPosition = new Vector3(0, 0, 0); // Set local position so that hat sits on top of the head.
        //helmet.localRotation = Quaternion.identity; // Zero hat's rotation.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    void Iterate(Transform transform, string ods)
    {
        string od = ods;
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (transform.name == "head")
                {
                    head = transform;
                }
                else
                {
                    Iterate(child, od + "-");
                }
            }
        }
        else if (transform.name == "head")
        {
                Debug.Log("found 2");
        }

    }*/
}
