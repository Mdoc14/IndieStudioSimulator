using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float forwardOffset = 0.2f;
    private IInteractable interactableOnSight;
    private RaycastHit hit;

    void Update()
    {
        IInteractable prevInteractable = interactableOnSight;
        if (Physics.Raycast(transform.position + forwardOffset*transform.forward, transform.forward, out hit, maxDistance))
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
        if (context.performed && GameObject.FindObjectOfType<PlayerMovement>().canMove) interactableOnSight?.Interact();
    }

    public void ResetInteractable()
    {
        interactableOnSight = null;
    }
}