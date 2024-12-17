using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class LightSwitch : AInteractable 
{
    // Start is called before the first frame update
    public event Action LightsOut;
    public event Action LightsOn;
    public float lightsOutProbability = 0.1f;
    public float checkInterval = 20.0f; //Tiempo que determina cada cuanto es posible que se apaguen las luces
    public bool IsOn { get; private set; } = true;

    private Dictionary<EmployeeBehaviour, string> _animations = new Dictionary<EmployeeBehaviour, string>();
    private Dictionary<EmployeeBehaviour, string> _barks = new Dictionary<EmployeeBehaviour, string>();
    private Dictionary<EmployeeBehaviour, bool> _navMeshAgents = new Dictionary<EmployeeBehaviour, bool>();
    private int _activeWorkers;

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
        StopEmployeeActivity();
    }

    public override void Repair()
    {
        ResetInteractable();
        RenderSettings.ambientIntensity = 1.0f;
        RenderSettings.reflectionIntensity = 1.4f;
        DynamicGI.UpdateEnvironment();
        Debug.Log("¡Luz arreglada!");
        IsOn = true;
        RestoreEmployeeActivity();
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

    private void StopEmployeeActivity()
    {
        _barks.Clear();
        _animations.Clear();
        _navMeshAgents.Clear();
        foreach(EmployeeBehaviour employee in GameObject.FindObjectsOfType<EmployeeBehaviour>())
        {
            _navMeshAgents.Add(employee, employee.GetComponent<NavMeshAgent>().enabled);
            _animations.Add(employee, employee.GetAnimation());
            _barks.Add(employee, employee.GetBark());
            if(employee.GetComponent<NavMeshAgent>().enabled) employee.GetComponent<NavMeshAgent>().isStopped = true;
            employee.enabled = false;
            employee.SetBark("Wait");
            employee.SetAnimation("Idle");
        }
        _activeWorkers = WorldManager.Instance.ActiveWorkers;
        WorldManager.Instance.SetActiveWorkers(0);
    }

    private void RestoreEmployeeActivity()
    {
        foreach (EmployeeBehaviour employee in _navMeshAgents.Keys)
        {
            if (employee.GetComponent<NavMeshAgent>().enabled) employee.GetComponent<NavMeshAgent>().isStopped = false;
            employee.enabled = true;
        }

        foreach (EmployeeBehaviour employee in _animations.Keys)
        {
            employee.SetAnimation(_animations[employee]);
        }

        foreach (EmployeeBehaviour employee in _barks.Keys)
        {
            employee.SetBark(_barks[employee]);
        }

        WorldManager.Instance.SetActiveWorkers(_activeWorkers);
    }
}
