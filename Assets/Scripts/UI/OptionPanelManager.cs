using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class OptionPanelManager : MonoBehaviour
{

    public static OptionPanelManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public RectTransform rt;
    public RectTransform parent;
    
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void SetPosition()
    {
        rt.localPosition = Input.mousePosition - parent.localPosition;
    }

    public void SetOptions(List<GameObject> options)
    {
        foreach (GameObject option in options)
        {
            option.transform.SetParent(transform);
        }
    }

    public void OpenDialogueWindow()
    {
        SetPosition();
        gameObject.SetActive(true);
    }

    private void ClearDialogueWindow()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void CloseDialogueWindow()
    {
        ClearDialogueWindow();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKeyDown && !EventSystem.current.IsPointerOverGameObject())
        {
            CloseDialogueWindow();
        }
    }

}

