using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(BasicObject))]
public class Interactable : MonoBehaviour, IInteractable
{

    [Header("Interactable")]
    protected GameObject optionItemPrefab;
    protected GameObject player;

    protected List<GameObject> options;

    void Start()
    {
        ImplementsInteractable();
        options = new List<GameObject>();
        optionItemPrefab = Resources.Load<GameObject>("UI/Option");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void CallPlayer(Vector3 position)
    {
        player.GetComponent<PlayerMotor>().MoveToObject(transform.position, true);
    }


    protected virtual void PerformInteraction()
    {
        RemoveEventListener();
        //perform default interaction
    }

    protected virtual void CreateOptions(Vector3 position)
    {
        AddWalkHereButton(position);
    }

    protected void AddWalkHereButton(Vector3 position)
    {
        GameObject option = Instantiate(optionItemPrefab);
        option.GetComponent<Text>().text = "Walk Here";
        option.GetComponent<Button>().onClick.AddListener(() => { OptionPanelManager.Instance.CloseDialogueWindow(); });
        option.GetComponent<Button>().onClick.AddListener(() => { CallPlayer(position); });
        options.Add(option);
    }

    public void ShowGUI(Vector3 position)
    {
        options.Clear();
        CreateOptions(position);
        OptionPanelManager.Instance.SetOptions(options);
        OptionPanelManager.Instance.OpenDialogueWindow();
    }

    protected void ImplementsInteractable()
    {
        if (this is IInteractable)
            GetComponent<BasicObject>().ImplementsInteractable();
    }

    public void Interact(Vector3 position)
    {
        //default interaction
        CallPlayer(position);
        AddEventListener();
    }

    protected void AddInteractionButton(Vector3 position, string buttonName)
    {
        GameObject option = Instantiate(optionItemPrefab);
        option.GetComponent<Text>().text = buttonName;
        option.GetComponent<Button>().onClick.AddListener(() => { OptionPanelManager.Instance.CloseDialogueWindow(); });
        option.GetComponent<Button>().onClick.AddListener(() => { Interact(position); });
        options.Add(option);
    }

    protected void AddEventListener()
    {
        player.GetComponent<PlayerMotor>().Reached += new DestinationReached(PerformInteraction);
    }

    protected void RemoveEventListener()
    {
        player.GetComponent<PlayerMotor>().Reached -= new DestinationReached(PerformInteraction);
    }

}
