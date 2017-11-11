using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighbours : MonoBehaviour
{
    bool debugInfo = false;                         // disable all debugs

    bool isReady = false;
    public GameObject[] neighbours = new GameObject[8];
    float shiftConstant = 320;
    RaycastHit[] rHit;

    Vector3[] neighboursPos = new Vector3[8];
    Vector3 actualPos;

    void Start()
    {

        InitNeighbours();
    }

    void InitNeighbours()
    {
        transform.tag = "Ground";

        if (InitPositions())
        {
            for (int i = 0; i < neighbours.Length; i++)
            {
                neighbours[i] = ShiftRay(neighboursPos[i]);
                if(debugInfo) Debug.Log("Added neighbour named: " + neighbours[i]);
            }
        }
        else
        {
            if (debugInfo) Debug.Log("Neighbours > Awake() > InitPositions() failed!");
        }
        isReady = true;
        if(debugInfo) Debug.Log("Neighbours from " + transform.name + " ready");
    }

    bool InitPositions()
    {
        actualPos = transform.position;
        float height = actualPos.y + 100f;
        neighboursPos[0] = new Vector3(actualPos.x - shiftConstant, height, actualPos.z);
        neighboursPos[1] = new Vector3(neighboursPos[0].x, height, neighboursPos[0].z - shiftConstant);
        neighboursPos[2] = new Vector3(neighboursPos[1].x + shiftConstant, height, neighboursPos[1].z);
        neighboursPos[3] = new Vector3(neighboursPos[2].x + shiftConstant, height, neighboursPos[2].z);
        neighboursPos[4] = new Vector3(neighboursPos[3].x, height, neighboursPos[3].z + shiftConstant);
        neighboursPos[5] = new Vector3(neighboursPos[4].x, height, neighboursPos[4].z + shiftConstant);
        neighboursPos[6] = new Vector3(neighboursPos[5].x - shiftConstant, height, neighboursPos[5].z);
        neighboursPos[7] = new Vector3(neighboursPos[6].x - shiftConstant, height, neighboursPos[6].z);
        return true;

    }

    GameObject ShiftRay(Vector3 pos)
    {
        rHit = Physics.RaycastAll(pos, Vector3.down, 300f);


        if (rHit.Length == 0)
        {
            if (debugInfo) Debug.Log(transform.name + ": Neighbours > ShiftRay() > No Neighbour");
            return new GameObject("NaN") ;
        }
        else
        {
            int it = 0;
            foreach (RaycastHit rH in rHit)
            {

                it++;
                if (rH.transform.tag == "Ground")
                {
                    //Debug.Log("Hit ground named: " + rH.transform.name);
                    return rH.transform.gameObject;
                }
                else
                {
                    if (debugInfo) Debug.Log(transform.name + ": Neighbours > ShiftRay() > No 'Ground' tag or another object! Iteration " + it);
                }
            }
        }


        return null;
    }

    public void ActivateChunk()
    {
        ChangeChunkState(true);
    }

    public void DeactivateChunk()
    {
        ChangeChunkState(false);
    }

    void ChangeChunkState(bool active)
    {
        if (!isReady) InitNeighbours();
        foreach (GameObject tile in neighbours)
        {
            if (tile.transform.name != "NaN")
            {
                tile.transform.parent.gameObject.SetActive(active);
            }
            
        }
        
    }

    public bool IsReady()
    {
        return isReady;
    }
}
