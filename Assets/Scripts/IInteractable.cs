using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Interact();

    public void HoverEnter();

    public void HoverExit();

    public void ResetInteractable();
}
