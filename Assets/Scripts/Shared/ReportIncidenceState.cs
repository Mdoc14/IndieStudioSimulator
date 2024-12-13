using CharactersBehaviour;
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
        actions.Add(new GoToPositionAction(agent, GameObject.Find("MaintenanceRoom").transform.position));
        actions.Add(new WaitingMaintenanceAction(agent));
        actions.Add(new GoToDeskAction(agent, GameObject.FindWithTag("IncidenceChair").GetComponent<Chair>()));
        //actions.Add(new UseBathroomAction(agent));
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
}
