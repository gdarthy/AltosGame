using System;
using UnityEngine;

public class TreeController : MonoBehaviour
{

    public int stage;

    public GameObject treeModel;

    public float growthInterval;
    private float actualTime;

    private GameObject LoadModel(string modelName)
    {
        return Resources.Load("Objects/Nature/Trees/" + modelName) as GameObject;
    }

    public void SetModel(int stage)
    {
        string modelName = "";
        if (treeModel == null)
        {
            modelName = transform.name.Substring(0, 5);
        }
        else
        {
            modelName = treeModel.transform.name.Substring(0, 5);
        }

        switch (stage)
        {
            case 1:
                treeModel = LoadModel(modelName + "Stage1");
                break;
            case 2:
                treeModel = LoadModel(modelName + "Stage2");
                break;
            case 3:
                treeModel = LoadModel(modelName + "Stage3");
                break;
            case 4:
                treeModel = LoadModel(modelName);
                break;
            case 11:
                treeModel = LoadModel(modelName + "Stage1Stub");
                break;
            case 12:
                treeModel = LoadModel(modelName + "Stage2Stub");
                break;
            case 13:
                treeModel = LoadModel(modelName + "Stage3Stub");
                break;
            case 14:
                treeModel = LoadModel(modelName + "Stub");
                break;
        }
        this.stage = stage;
    }

    public GameObject GetModel(Transform parent, Vector3 position)
    {
        return Instantiate(treeModel, parent);
    }

    public void ChopTree()
    {
        SetModel(stage + 10);
    }

    void Start()
    {
        actualTime = growthInterval;
        SetModel(stage);
    }
    
    void Update()
    {
        if (stage != 4)
        {
            //TreeGrowth();
        }
    }

    void TreeGrowth()
    {
        actualTime -= Time.deltaTime;

        if (actualTime <= 0)
        {
            stage += 1;
            SetModel(stage);
            actualTime = growthInterval;
        }
    }
}