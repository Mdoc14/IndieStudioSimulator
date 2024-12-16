using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LightSwitch : AInteractable 
{
    // Start is called before the first frame update
    public event Action LightsOut;
    public event Action LightsOn;
    public float lightsOutProbability = 0.1f;
    public float checkInterval = 20.0f; //Tiempo que determina cada cuanto es posible que se apaguen las luces
    public bool IsOn { get; private set; } = true;

    void Start()
    {
        //Inicia la corrutina para apagar las luces automáticamente
        StartCoroutine(LightsGoOut());
    }
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Se ha ido la luz!");
        RenderSettings.ambientIntensity = 0.0f;
        RenderSettings.reflectionIntensity = 0.4f;
        DynamicGI.UpdateEnvironment();
        LightsOut?.Invoke();
        IsOn = false;
    }

    public override void Repair()
    {
        ResetInteractable();
        RenderSettings.ambientIntensity = 1.0f;
        RenderSettings.reflectionIntensity = 1.4f;
        DynamicGI.UpdateEnvironment();
        Debug.Log("¡Luz arreglada!");
        IsOn = true;
        LightsOn?.Invoke();
    }

    IEnumerator LightsGoOut() {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval); //Espera el intervalo de tiempo

            //Genera un número aleatorio para determinar si las luces se apagan
            float randomValue = UnityEngine.Random.Range(0f, 1f);
            if (randomValue <= lightsOutProbability && IsOn)
            {
                //Apaga las luces si el número está dentro de la probabilidad
                Interact();
            }
        }
    }
}
