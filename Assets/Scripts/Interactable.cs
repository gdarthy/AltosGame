using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Interactable")]
    protected GameObject optionItemPrefab;
    protected List<GameObject> options;
    public ToolType requiredTool { get; protected set;}

    protected virtual void Awake()
    {
        options = new List<GameObject>();
        optionItemPrefab = Resources.Load<GameObject>("UI/Option");
        gameObject.layer = 11;
    }
    public void ShowGUI()
    {
        options.Clear();
        CreateOptions();
        OptionPanelManager.Instance.SetOptions(options);
        OptionPanelManager.Instance.OpenDialogueWindow();
    }

    protected virtual void CreateOptions()
    {
        AddWalkHereButton();
    }
    public virtual void Interact(GameObject character)
    {

    }
    protected void AddWalkHereButton()
    {
        GameObject option = Instantiate(optionItemPrefab);
        option.GetComponent<Text>().text = "Walk Here";
        option.GetComponent<Button>().onClick.AddListener(() => { OptionPanelManager.Instance.CloseDialogueWindow(); });
        //option.GetComponent<Button>().onClick.AddListener(() => { CallPlayer(); });
        options.Add(option);
    }
    protected void AddInteractionButton(string buttonName)
    {
        GameObject option = Instantiate(optionItemPrefab);
        option.GetComponent<Text>().text = buttonName;
        option.GetComponent<Button>().onClick.AddListener(() => { OptionPanelManager.Instance.CloseDialogueWindow(); });
        //option.GetComponent<Button>().onClick.AddListener(() => { Interact(); });
        options.Add(option);
    }
}
