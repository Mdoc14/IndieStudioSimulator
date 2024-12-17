using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BathroomInteractable : AInteractable
{
    [SerializeField] private GameObject _waterParticles;
    public bool broken { get; private set; } = false;
    public event Action OnBreak;

    public override void Interact()
    {
        base.Interact();
        broken = true;
        OnBreak?.Invoke();
        _waterParticles.SetActive(true);
    }
    
    public override void Repair()
    {
        base.Repair();
        base.ResetInteractable();
        broken = false;
        _waterParticles.SetActive(false);
        GetComponent<Chair>().selected = false;
    }
}
