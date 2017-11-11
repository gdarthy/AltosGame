using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    [Header("General")]
    public GameObject targetForest;
    public List<GameObject> treeModels;
    public AnimationCurve probability;

    [Header("Level 1")]
    [SerializeField]
    private float _radius;
    public Color level1Color;
    public int treeCount;

    [Header("Level 2")]
    [SerializeField]
    private float _radius2;
    public Color level2Color;
    public int treeCount2;

    RaycastHit rHit;

    // Use this for initialization
    void Start()
    {
        treeModels = new List<GameObject>();
    }

    void OnDrawGizmos()
    {
        level1Color = Color.green;
        level2Color = Color.red;
        Handles.color = level1Color;
        Handles.DrawWireDisc(transform.position, transform.up, _radius);
        Handles.color = level2Color;
        Handles.DrawWireDisc(transform.position, transform.up, _radius2);
    }

    public void Instantiate()
    {
        if (treeModels.Count > 0)
        {
            if (targetForest != null && targetForest.transform.tag == "ForestController")
            {
                CreateTrees(treeCount, _radius);
                CreateTrees(treeCount2, _radius2);
            }
            else
            {
                Debug.Log("No Target Forest attached, or attached object has not proper tag");
            }
        }
        else
        {
            Debug.Log("No Tree Models attached");
        }
    }

    void CreateTrees(int treeCount, float radius)
    {
        GameObject tree;
        for (int i = 0; i < treeCount; i++)
        {
            tree = Instantiate(treeModels[Random.Range(0, treeModels.Count)], NewPosition(radius), Quaternion.Euler(-90, Random.Range(0, 360), 0)); 
            TreeController tc = tree.transform.GetComponent<TreeController>();
            tc.SetModel(Mathf.RoundToInt(probability.Evaluate(Random.Range(0, 100))));
            tc.GetModel(tree.transform, tree.transform.position);
            tree.transform.parent = targetForest.transform;
        }
    }

    Vector3 NewPosition(float radius)
    {
        Vector3 position = transform.position;
        Vector2 coords = Random.insideUnitCircle * radius;

        if (Physics.Raycast(new Vector3(transform.position.x + coords.x, transform.position.y + 10f, transform.position.z + coords.y), Vector3.down, out rHit, 50f))
        {
            if (rHit.transform.tag == "Ground")
            {
                position = rHit.point;
            }
            else
            {
                //position = NewPosition(radius);
            }
        }
        else
        {
            Debug.Log("No Ray Hit, calling again");
            position = NewPosition(radius);
        }
        return position;
    }

    public void Clear()
    {
        List<GameObject> l = new List<GameObject>();
        if (targetForest != null && targetForest.transform.tag == "ForestController")
        {
            for (int i = 0; i < targetForest.transform.childCount; i++)
            {
                l.Add(targetForest.transform.GetChild(i).gameObject);
            }
            foreach (GameObject g in l)
            {
                DestroyImmediate(g);
            }
        }
        else
        {
            Debug.Log("No Target Forest attached, or attached object has not proper tag");
        }
    }

    public float Radius
    {
        get
        {
            return _radius;
        }

        set
        {
            _radius = value;
        }
    }

    public float Radius2
    {
        get
        {
            return _radius2;
        }

        set
        {
            _radius2 = value;
        }
    }
}

[CustomEditor(typeof(ForestGenerator))]
public class ForestManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        ForestGenerator myScript = (ForestGenerator)target;
        if (GUILayout.Button("Create"))
        {
            myScript.Instantiate();
        }
        if (GUILayout.Button("Clear forest"))
        {
            myScript.Clear();
        }
    }
}
