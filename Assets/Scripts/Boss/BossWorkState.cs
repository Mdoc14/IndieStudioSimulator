using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWorkState : AState
{
    CompositeAction _workAction;
    private StateMachine _context;
    bool _forceUseComputer = false;
    public BossWorkState(StateMachine sm, IAgent agent, bool forceUseComputer = false) : base(sm, agent) { _context = sm; _forceUseComputer = forceUseComputer; }

    public override void Enter()
    {
        Debug.Log("ENTRANDO EN ESTADO DE TRABAJO...");
        context.PreviousStates.Push(this);
        //El jefe va a su silla y despu�s utiliza su ordenador o su tel�fono
        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToDeskAction(agent)); 
        if (Random.Range(0, 2) == 0 || _forceUseComputer) actions.Add(new WorkAction(agent, _context));
        else actions.Add(new CallAction(agent));
        _workAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("ESTADO DE TRABAJO FINALIZADO");
    }

    public override void FixedUpdate()
    {
        _workAction?.FixedUpdate();
    }

    public override void Update()
    {
        _workAction?.Update();
        if (_workAction.Finished)
        {
            agent.GetChair().Leave();
            float rand = Random.Range(0.0f, 1.0f);
            if (rand < 0.1f) context.State = new BathroomState(context, agent, new BossWorkState(context, agent));
            else if (rand < 0.35f) context.State = new BossReunionState(context, agent);
            else context.State = new PatrolState(context, agent);
        }
    }
}
