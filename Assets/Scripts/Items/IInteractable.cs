using UnityEngine;

public interface IInteractable {

    void Interact(Vector3 position);

    void ShowGUI(Vector3 position);

    InteractableType GetInteractableType();
}
