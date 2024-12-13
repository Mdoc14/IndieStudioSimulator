using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : AInteractable {
    // Start is called before the first frame update
    public event Action LightsOut;
    void Start()
    {
        
    }
    public override void Interact()
    {
        Debug.Log("Se ha ido la luz!");
        RenderSettings.ambientIntensity = 0.0f;
        RenderSettings.reflectionIntensity = 0.4f;
        DynamicGI.UpdateEnvironment();
        LightsOut?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
