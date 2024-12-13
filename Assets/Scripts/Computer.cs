using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Computer : AInteractable
{
    [SerializeField] private GameObject _smokeParticles;
    [SerializeField] private Screen[] screens;

    protected override void Awake()
    {
        base.Awake();
        SetScreensContent(ScreenContent.Working);
    }

    public override void Interact()
    {
        base.Interact();
        //Poner sistemas de partículas para expresar que se ha roto
        SetScreensContent(ScreenContent.Off);
        _smokeParticles.SetActive(true);
    }

    public void Repair()
    {
        base.ResetInteractable();
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
