using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUIMenuManager : MonoBehaviour
{

    public static PlayerUIMenuManager Instance { get; private set; }

    public GameObject headPosition;

    public RectTransform parentRectTransform;
    public RectTransform rt;

    private Vector2 result;
    private Vector3 pos;

    void Awake()
    {
        if (Instance == null)
            //if not, set instance to this
            Instance = this;
        //If instance already exists and it's not this:
        else if (Instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);
    }

    public void Repos()
    {
        pos = Camera.main.WorldToScreenPoint(headPosition.transform.position);
        rt.localPosition = new Vector3(0, pos.y - parentRectTransform.localPosition.y, 0);
    }


    public void MoveOut()
    {
        rt.localPosition = new Vector3(0, -parentRectTransform.localPosition.y * 2, 0);
    }
}
