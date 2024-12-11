using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammerWorkState : AState
{
    public ProgrammerWorkState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _workAction;

    public override void Enter()
    {
        Debug.Log("PROGRAMADOR ENTRANDO EN ESTADO DE TRABAJO...");
        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToDeskAction(agent));
        actions.Add(new ProgrammingAction(agent));
        _workAction = new CompositeAction(actions);
        
    }

    public override void Exit()
    {
        Debug.Log("PROGRAMADOR HA SALIDO DE ESTADO DE TRABAJO");
    }

    public override void FixedUpdate()
    {
        _workAction?.FixedUpdate();
    }

    public override void Update()
    {
        _workAction.Update();
        if (_workAction.HasFinished)
        {
            agent.GetChair().Leave();
            agent.SetAgentVariable("Motivation", agent.GetAgentVariable("Motivation") - Random.Range(0f, 0.1f));
            agent.SetAgentVariable("Boredom", agent.GetAgentVariable("Boredom") + Random.Range(0f, 0.1f));
            agent.SetAgentVariable("Stress", agent.GetAgentVariable("Stress") + Random.Range(0f, 0.1f));
            context.State = new BathroomState(context, agent, new ProgrammerWorkState(context, agent));
        }
    }
}
