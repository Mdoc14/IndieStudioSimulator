using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPCState : AState
{
    public PlayPCState(StateMachine sm, IAgent agent) : base(sm, agent) { _machine = sm; }
    CompositeAction _playPCAction;
    StateMachine _machine;

    public override void Enter()
    {
        Debug.Log("PROGRAMADOR ENTRANDO EN ESTADO DE JUGAR...");
        (agent as EmployeeBehaviour).isSlacking = true;

        List<IAction> actions = new List<IAction>();
        if (!agent.GetChair().IsOccupied())
        {
            actions.Add(new GoToDeskAction(agent));
        }
        actions.Add(new PlayPCAction(agent, _machine));
        _playPCAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("PROGRAMADOR HA SALIDO DE ESTADO DE JUGAR");
        (agent as EmployeeBehaviour).isSlacking = false;
        agent.GetComputer().SetScreensContent(ScreenContent.Off);
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
