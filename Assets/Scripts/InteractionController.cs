using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    private IInteractable interactableOnSight;
    private RaycastHit hit;

    void Update()
    {
        IInteractable prevInteractable = interactableOnSight;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            interactableOnSight = hit.transform.GetComponent<IInteractable>();
        }
        else interactableOnSight = null;

        if (interactableOnSight != prevInteractable)
        {
            interactableOnSight?.HoverEnter();
            prevInteractable?.HoverExit();
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed) interactableOnSight?.Interact();
    }
}