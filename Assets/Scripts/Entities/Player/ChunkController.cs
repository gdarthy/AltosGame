using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour {


    RaycastHit rHitNew;
    RaycastHit rHitActual;
    RaycastHit[] allHits;
    bool isReady = false;
    void Start()
    {

    }

    void Update() {

        if(GameObject.FindGameObjectWithTag("World").GetComponent<ZoneStatus>().IsReady() && !isReady)
        {
            Debug.Log("ChunkController > Update() > World is ready");
            isReady = true;
            SetDefaultChunk();
        }

        allHits = Physics.RaycastAll(transform.position, Vector3.down, 20f);

        foreach (RaycastHit r in allHits)
        {
            if (r.transform.tag == "Ground")
            {
                rHitNew = r;
                //Debug.Log(rHitNew.transform.name);
                if (rHitActual.transform == null)
                {
                    rHitNew.transform.GetComponent<Neighbours>().ActivateChunk();
                    
                    rHitActual = rHitNew;
                }
                else if (rHitNew.transform.name != rHitActual.transform.name)
                {

                    rHitActual.transform.GetComponent<Neighbours>().DeactivateChunk();
                    rHitNew.transform.GetComponent<Neighbours>().ActivateChunk();
                    rHitNew.transform.parent.gameObject.SetActive(true);
                    rHitActual = rHitNew;
                }
            }
        }
    }

    void SetDefaultChunk()
    {
        allHits = Physics.RaycastAll(transform.position, Vector3.down, 20f);

        foreach (RaycastHit r in allHits)
        {
            if (r.transform.tag == "Ground")
            {
                rHitNew = r;
            }
        }

        GameObject[] containers = GameObject.FindGameObjectsWithTag("Container");
        foreach(GameObject c in containers)
        {
            c.SetActive(false);
        }
        
        rHitNew.transform.GetComponent<Neighbours>().transform.parent.gameObject.SetActive(true);
        rHitNew.transform.GetComponent<Neighbours>().ActivateChunk();
    }
}
