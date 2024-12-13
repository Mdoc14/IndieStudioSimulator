using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Computer : AInteractable
{
    [SerializeField] private GameObject _smokeParticles;
    [SerializeField] private Screen[] screens;
    public bool broken { get; private set; } = false;
    public event Action OnBreak;

    public override void Interact()
    {
        base.Interact();
        broken = true;
        OnBreak?.Invoke();
        SetScreensContent(ScreenContent.Off);
        _smokeParticles.SetActive(true);
    }

    public void Repair()
    {
        base.ResetInteractable();
        broken = false;
        _smokeParticles.SetActive(false);
    }

    public void SetScreensContent(ScreenContent state)
    {
        foreach(Screen screen in screens)
        {
            screen.SetScreenVideo(state);
        }
    }    
}
