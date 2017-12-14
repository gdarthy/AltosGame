using UnityEngine.UI;
using UnityEngine;

public class TreeHandler : Extractable {

    protected override void Start()
    {
        base.Start();
        requiredTool = ToolType.Axe;
    }
    protected override void PerformInteraction()
    {
        Debug.Log("Interacted with tree");
        player.GetComponent<CharacterAnimation>().CuttingOneHand(true);
        RemoveEventListener();
    }

    protected override void CreateOptions(Vector3 position)
    {
        AddWalkHereButton(position);
        AddInteractionButton(position, "Cut Down");
    }

    public override ToolType GetRequiredTool()
    {
        return requiredTool;
    }
}
