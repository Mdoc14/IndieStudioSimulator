using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction : ASimpleAction
{
    private float _workTime;

    public CallAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        _workTime = Random.Range(5, agent.GetAgentVariable("MaxWorkTime"));
        Debug.Log("El jefe está llamando por teléfono...");
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        _workTime -= Time.deltaTime;
        if (_workTime <= 0)
        {
            Debug.Log("Fin de la llamada");
            finished = true;
        }
    }
}
