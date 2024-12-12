using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammerWalkState : AState
{
    public ProgrammerWalkState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    ASimpleAction _workAction;

    public override void Enter()
    {
        Debug.Log("PROGRAMADOR ENTRANDO EN ESTADO DE CAMINAR...");
        
        _workAction = new GoToPositionAction(agent, new Vector3(7f,0f,2f));
    }

    public override void Exit()
    {
        Debug.Log("PROGRAMADOR HA SALIDO DE ESTADO DE CAMINAR");
    }

    public override void FixedUpdate()
    {
        _workAction?.FixedUpdate();
    }

    public override void Update()
    {
        _workAction.Update();
        if (_workAction.Finished)
        {
            context.State = new ProgrammerWorkState(context, agent);
        }
    }
}
