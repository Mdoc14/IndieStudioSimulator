using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPCState : AState
{
    public PlayPCState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _playPCAction;
    EmployeeBehaviour _employeeBehaviour;

    public override void Enter()
    {
        Debug.Log("PROGRAMADOR ENTRANDO EN ESTADO DE JUGAR...");
        List<IAction> actions = new List<IAction>();
        if (!agent.GetChair().IsOccupied())
        {
            actions.Add(new GoToDeskAction(agent));
        }
        actions.Add(new PlayPCAction(agent));
        _employeeBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        _playPCAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("PROGRAMADOR HA SALIDO DE ESTADO DE JUGAR");
    }

    public override void FixedUpdate()
    {
        _playPCAction?.FixedUpdate();
    }

    public override void Update()
    {
        _playPCAction.Update();
        if (_playPCAction.Finished)
        {
            context.State = new CheckEmployeeNecessitiesState(context, agent, new ProgrammerWorkState(context, agent));
        }
    }

}
