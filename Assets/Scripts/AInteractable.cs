using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Soy un interactuable");
    }

    public void HoverEnter()
    {
        Debug.Log("HoverEntered");
    }

    public void HoverExit()
    {
        Debug.Log("HoverExited");
    }
}
