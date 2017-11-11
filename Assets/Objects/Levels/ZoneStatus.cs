using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneStatus : MonoBehaviour {

    bool isReady = false;
    int ready = 0;

    public int readyLvl;
    public string tagLvl;
    GameObject[] containers;

	void Update () {

        containers = GameObject.FindGameObjectsWithTag(tagLvl);
        foreach (GameObject container in containers)
        {
            if (tagLvl == "Container")
            {
                if (container.GetComponentInChildren<Neighbours>().IsReady())
                {
                    ready++;
                }
            }else
            {
                if (container.GetComponent<ZoneStatus>().IsReady())
                {
                    ready++;
                }
            }
        }

        if (ready == readyLvl)
        {
            isReady = true;
            //Debug.Log(transform.tag + " " + transform.name + " ready");
        }
	}

    public bool IsReady()
    {
        return isReady;
    }
}
