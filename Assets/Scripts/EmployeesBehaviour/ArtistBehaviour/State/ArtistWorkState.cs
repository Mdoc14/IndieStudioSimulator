using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtistWorkState : AState
{
    public ArtistWorkState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _workAction;
    bool _alreadySubscribed;

    public override void Enter()
    {
        Debug.Log("ARTISTA ENTRANDO EN ESTADO DE TRABAJO...");
        List<IAction> actions = new List<IAction>();
        if (!agent.GetChair().IsOccupied())
        {
            actions.Add(new GoToDeskAction(agent));
        }
        actions.Add(new DrawingAction(agent, context));
        _workAction = new CompositeAction(actions);
        (agent as EmployeeBehaviour).working = true;
    }

    public override void Exit()
    {
        Debug.Log("ARTISTA HA SALIDO DE ESTADO DE TRABAJO");
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
            WorldManager.Instance.SetWorkerActivity(true);
            _alreadySubscribed = true;
        }

        if (_workAction.Finished)
        {

            WorldManager.Instance.SetWorkerActivity(false);
            context.State = new CheckEmployeeNecessitiesState(context, agent, new ArtistWorkState(context, agent));
        }
    }
}
