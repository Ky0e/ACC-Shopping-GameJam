using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class Player_Interactor : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Refrence Components")] private InputReader input;
    [field: SerializeField, BoxGroup("Interaction Properties")] private float interactionRadius = 1f;
    [field: SerializeField, BoxGroup("Interaction Properties")] private LayerMask interactionLayer;
    [field: SerializeField, BoxGroup("Debug Properties")] private bool enableDebug = true;
    private bool isInteracting = false;
    private List<Collider> currentColliders = new();
    private void Update() { Interact(); }

    private void Interact()
    {
        // OverlapSphere to find colliders in the interaction radius
        Collider[] _interactableCollisions = Physics.OverlapSphere(transform.position, interactionRadius, interactionLayer);

        // Loop through each collider found in the interaction radius
        foreach (var _collision in _interactableCollisions)
        {
            // Check if the collider has the IInteractable component
            if (_collision.TryGetComponent(out IInteractable _component))
            {
                // Call OnInteractionAreaEntered on each interactable object
                _component.OnInteractionAreaEntered();
                currentColliders.Add(_collision);
                // If the player presses the interact button, call OnInteract
                if (input.Interact && !isInteracting)
                {
                    _component.OnInteract();
                    isInteracting = true; // Set interacting flag
                }
                else if (!input.Interact && isInteracting)
                {
                    isInteracting = false; // Reset interacting flag
                }
            }
        }

        // If there are no colliders in the interaction radius, call OnInteractionAreaExited on all interactable objects
        if (_interactableCollisions.Length == 0)
        {
            if (currentColliders.Count <= 0) { return; }
            for (int i = currentColliders.Count - 1; i > 0; i--)
            {
                Debug.Log(currentColliders[i].name);
                if (currentColliders[i].TryGetComponent(out IInteractable _component))
                {
                    _component.OnInteractionAreaExited();
                    currentColliders.Remove(currentColliders[i]);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (this.gameObject.activeInHierarchy && enableDebug)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(this.transform.position, interactionRadius);
        }
    }
}
