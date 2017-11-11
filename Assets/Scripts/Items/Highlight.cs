using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour {

    Color [] temp;
    Color objectColor;
    int x = 0;

    void Start()
    {

        temp = new Color[GetComponents<Renderer>().Length + 1];
    }

	void OnMouseEnter()
    {
        x = 0;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Renderer>() != null)
            {
                temp[x] = child.GetComponent<Renderer>().materials[0].color;
                objectColor = child.GetComponent<Renderer>().materials[0].color;
                objectColor.r = 0;
                child.GetComponent<Renderer>().materials[0].color = objectColor;
                x++;
            }
            
        }
    }

    void OnMouseExit()
    {
        x = 0;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Renderer>() != null)
            {
                child.GetComponent<Renderer>().materials[0].color = temp[x];
                x++;
            }
            
        }
    }

}
