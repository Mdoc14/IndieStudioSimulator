using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFixLight : ASimpleAction
{
    private float _fixingTime;
    public ActionFixLight(IAgent agent) : base(agent) { }
    public override void Enter()
    {
        base.Enter();
        _fixingTime = Random.Range(5, 10);
        Debug.Log("Está arreglando la luz...");
        agent.SetBark("Repair");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        _fixingTime -= Time.deltaTime;
        if (_fixingTime <= 0)
        {
            RenderSettings.ambientIntensity = 1.0f;
            RenderSettings.reflectionIntensity = 1.4f;
            DynamicGI.UpdateEnvironment();
            Debug.Log("¡Luz arreglada!");

            finished = true;
        }
    }
}
