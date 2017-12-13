using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnableTreeInEditor : MonoBehaviour
{

    void Awake()
    {
        if (transform.childCount == 0)
        {
            GetComponent<TreeController>().SetModel(Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Probability").GetComponent<ForestGenerator>().probability.Evaluate(Random.Range(0, 100))));
            GetComponent<TreeController>().GetModel(transform, transform.position);
        }
    }
}
