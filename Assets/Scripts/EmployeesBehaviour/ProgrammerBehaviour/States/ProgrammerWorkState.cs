using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammerWorkState : AState
{
    public ProgrammerWorkState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _workAction;
    EmployeeBehaviour _employeeBehaviour;
    bool _alreadySubscribed;

    public override void Enter()
    {
        Debug.Log("PROGRAMADOR ENTRANDO EN ESTADO DE TRABAJO...");
        WorldManager.Instance.SetWorkerActivity(true);
        context.PreviousStates.Push(this);
        if ((agent as AgentBehaviour).currentIncidence != null) context.State = new ReportIncidenceState(context, agent);
        List<IAction> actions = new List<IAction>();
        if (!agent.GetChair().IsOccupied())
        {
            actions.Add(new GoToDeskAction(agent));
        }
        actions.Add(new ProgrammingAction(agent, context));
        _employeeBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        _workAction = new CompositeAction(actions);
        (agent as EmployeeBehaviour).working = true;
    }

    public override void Exit()
    {
        Debug.Log("PROGRAMADOR HA SALIDO DE ESTADO DE TRABAJO");
        WorldManager.Instance.SetWorkerActivity(false);
        (agent as EmployeeBehaviour).working = false;
        _alreadySubscribed = false;
        agent.GetComputer().SetScreensContent(ScreenContent.Off);
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
            _alreadySubscribed = true;
        }

        if (_workAction.Finished)
        {

            context.State = new CheckEmployeeNecessitiesState(context, agent, new ProgrammerWorkState(context, agent));
        }
    }
}
