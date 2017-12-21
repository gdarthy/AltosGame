using UnityEngine.UI;
using UnityEngine;

public class TreeInteractable : Extractable {

    protected override void Awake()
    {
        base.Awake();
        requiredTool = ToolType.Axe;
    }
    public override void Interact(GameObject character)
    {
        
        character.GetComponent<CharacterAnimation>().CuttingRightHand(true);
        GetComponent<TreeExtracting>().StartExtracting();
        //RemoveEventListener();
    }

    protected override void CreateOptions()
    {
        AddWalkHereButton();
        AddInteractionButton("Cut Down");
    }
}
