using UnityEngine;
using UnityEditor;
using System;

public class BasicObject : MonoBehaviour
{

    OptionPanelManager opm;

    [Header("BasicObject")]
    //[SerializeField]
    //private float _stoppingDistance;

    bool implementsInteractable;
    bool rayCasted;
    private bool isTransparent;
    //public bool debugStoppingDistance = false;
    
    // Get set

   /* public float StoppingDistance
    {
        get
        {
            return _stoppingDistance;
        }
        set
        {
            _stoppingDistance = value;
        }
    }*/

    /*void OnDrawGizmos()
    {
        if (debugStoppingDistance)
        {
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(transform.position, Vector3.up, StoppingDistance);
        }
    }*/

    protected virtual void Start() { }

    protected virtual void Awake()
    {
        implementsInteractable = false;
    }

    protected void Update()
    {
        if (isTransparent)
        {
            SetOpaque();
        }
    }

    public void SetTransparent()
    {
        Material material = transform.GetComponent<Renderer>().material;
        material.SetFloat("_Mode", 2);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        material.SetColor("_Color", new Color(1, 1, 1, 0.3f));
        isTransparent = true;
    }

    public void SetOpaque()
    {
        Material material = GetComponent<Renderer>().material;
        material.SetFloat("_Mode", 0);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
        material.SetColor("_Color", new Color(1, 1, 1, 1));
        isTransparent = false;
    }
    public void HideRoof()
    {
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
    }

    public void ShowRoof()
    {
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
    }

    protected void ImplementsInteractable()
    {
        implementsInteractable = true;
    }

    public bool IsInteractable()
    {
        return implementsInteractable;
    }

    public static GameObject GetObjectFromResources(ItemHolder item)
    {
        return Resources.Load<GameObject>("Objects/Items/" + item.itemType + "/" + item.itemResource);
    }
}
