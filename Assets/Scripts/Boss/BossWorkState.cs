using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWorkState : AState
{
    public BossWorkState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _workAction;

    public override void Enter()
    {
        Debug.Log("ENTRANDO EN ESTADO DE TRABAJO...");
        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToDeskAction(agent));
        if (Random.Range(0, 2) == 0) actions.Add(new WorkAction(agent));
        else actions.Add(new CallAction(agent));
        _workAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("ESTADO DE TRABAJO ABANDONADO");
    }

    public override void FixedUpdate()
    {
        _workAction?.FixedUpdate();
    }

    public override void Update()
    {
        _workAction?.Update();
    }
}
