using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammerWorkState : AState
{
    public ProgrammerWorkState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _workAction;
    bool _alreadySubscribed;

    public override void Enter()
    {
        Debug.Log("PROGRAMADOR ENTRANDO EN ESTADO DE TRABAJO...");
        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToDeskAction(agent));
        actions.Add(new ProgrammingAction(agent));
        _workAction = new CompositeAction(actions);
        (agent as EmployeeBehaviour).working = true;
    }

    public override void Exit()
    {
        Debug.Log("PROGRAMADOR HA SALIDO DE ESTADO DE TRABAJO");
        (agent as EmployeeBehaviour).working = false;
        _alreadySubscribed = false;
    }

    public override void FixedUpdate()
    {
        _workAction?.FixedUpdate();
    }

    public override void Update()
    {
        _workAction.Update();
        if (!_alreadySubscribed && WorldManager.Instance != null) 
        { 
            
            WorldManager.Instance.SetWorkerActivity(true);
            _alreadySubscribed = true;
        }

        if (_workAction.Finished)
        {
            agent.GetChair().Leave();
            if (agent.GetAgentVariable("Motivation") > 0.1f) agent.SetAgentVariable("Motivation", agent.GetAgentVariable("Motivation") - Random.Range(0f, 0.1f));
            if (agent.GetAgentVariable("Boredom") < 0.9f) agent.SetAgentVariable("Boredom", agent.GetAgentVariable("Boredom") + Random.Range(0f, 0.1f));
            if (agent.GetAgentVariable("Stress") < 0.9f) agent.SetAgentVariable("Stress", agent.GetAgentVariable("Stress") + Random.Range(0f, 0.1f));
            WorldManager.Instance.SetWorkerActivity(false);
            context.State = new BathroomState(context, agent, new ProgrammerWorkState(context, agent));
        }
    }

    
}
