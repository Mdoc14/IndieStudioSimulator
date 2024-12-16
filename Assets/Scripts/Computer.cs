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
    private ScreenContent prevContent = ScreenContent.Off;

    private void Start()
    {
        GameObject.FindObjectOfType<LightSwitch>().LightsOut += OnLightsOut;
        GameObject.FindObjectOfType<LightSwitch>().LightsOn += OnLightsOn;
    }

    public override void Interact()
    {
        base.Interact();
        broken = true;
        OnBreak?.Invoke();
        SetScreensContent(ScreenContent.Off);
        _smokeParticles.SetActive(true);
    }

    public override void Repair()
    {
        base.ResetInteractable();
        broken = false;
        _smokeParticles.SetActive(false);
    }

    public void SetScreensContent(ScreenContent state)
    {
        prevContent = state;
        foreach(Screen screen in screens)
        {
            screen.SetScreenVideo(state);
        }
    }    

    private void OnLightsOut()
    {
        ScreenContent c = prevContent; //Se guarda porque SetScreensContent sobreescribe prevContent
        SetScreensContent(ScreenContent.Off);
        prevContent = c;
    }

    private void OnLightsOn()
    {
        SetScreensContent(prevContent);
    }
}
