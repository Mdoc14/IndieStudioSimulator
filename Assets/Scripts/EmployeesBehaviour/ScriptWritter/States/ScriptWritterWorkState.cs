using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptWritterWorkState : AState
{
    public ScriptWritterWorkState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _workAction;
    bool _alreadySubscribed;

    public override void Enter()
    {
        Debug.Log("GUIONISTA ENTRANDO EN ESTADO DE TRABAJO...");
        List<IAction> actions = new List<IAction>();
        if (!agent.GetChair().IsOccupied())
        {
            actions.Add(new GoToDeskAction(agent));
        }
        actions.Add(new WrittingAction(agent, context));
        _workAction = new CompositeAction(actions);
        (agent as EmployeeBehaviour).working = true;
    }

    public override void Exit()
    {
        Debug.Log("GUIONISTA HA SALIDO DE ESTADO DE TRABAJO");
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

            WorldManager.Instance.SetWorkerActivity(false);
            context.State = new CheckEmployeeNecessitiesState(context, agent, new ScriptWritterWorkState(context, agent));
        }
    }
}
