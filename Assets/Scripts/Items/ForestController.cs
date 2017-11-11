using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ForestController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Clear()
    {
        List<GameObject> l = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            l.Add(transform.GetChild(i).gameObject);
        }
        foreach (GameObject g in l)
        {
            DestroyImmediate(g);
        }

    }
}

[CustomEditor(typeof(ForestController))]
public class ForestControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        ForestController myScript = (ForestController)target;

        if (myScript.transform.childCount > 0 && GUILayout.Button("Clear forest"))
        {
            myScript.Clear();
        }
    }
}