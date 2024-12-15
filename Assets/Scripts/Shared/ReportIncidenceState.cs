using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportIncidenceState : AState
{
    AState nextState;
    CompositeAction _incidenceAction;

    public ReportIncidenceState(StateMachine sm, IAgent agent, AState nextState = null) : base(sm, agent)
    {
        this.nextState = nextState;
    }

    public override void Enter()
    {
        //El estado de ir al baño se divide en tres acciones: ir al baño, comprobar si hay un retrete libre y usarlo
        List<IAction> actions = new List<IAction>();
        actions.Add(new LookIncidenceAction(agent));
        actions.Add(new GoToPositionAction(agent, GameObject.FindWithTag("IncidenceChair").transform.position));
        actions.Add(new WaitingMaintenanceAction(agent));
        actions.Add(new GoToDeskAction(agent, GameObject.FindWithTag("IncidenceChair").GetComponent<Chair>()));
        actions.Add(new TalkAction(agent, TalkCondition(), () => GameObject.FindObjectOfType<MaintenanceBehaviour>().SetCurrentIncidence(agent.GetComputer())));
        actions.Add(new FollowAgentAction(agent, GameObject.FindObjectOfType<MaintenanceBehaviour>(), FollowCondition()));
        _incidenceAction = new CompositeAction(actions);
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        _incidenceAction?.FixedUpdate();
    }

    public override void Update()
    {
        _incidenceAction?.Update();
        if (_incidenceAction.Finished && nextState != null)
        {
            context.State = nextState;
        }
    }

    private Func<bool> TalkCondition()
    {
        return () =>
        {
            if (!GameObject.FindWithTag("MaintenanceChair").GetComponent<Chair>().IsOccupied())
            {
                return true;
            }
            return false;
        };
    }

    private Func<bool> FollowCondition()
    {
        return () =>
        {
            if (!agent.GetComputer().broken)
            {
                return true;
            }
            return false;
        };
    }
}
