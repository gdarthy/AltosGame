using UnityEngine.UI;
using UnityEngine;

public class TreeHandler : Interactable {

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
}
