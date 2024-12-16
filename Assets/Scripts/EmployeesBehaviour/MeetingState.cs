using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingState : AState
{
    public MeetingState(StateMachine sm, IAgent agent, AState nextState) : base(sm, agent)
    {
        this.nextState = nextState;
    }
    AState nextState;
    CompositeAction _workAction;
    bool _onlyOnce; // Comprueba si se ha asignado el bark y la animación de esperar para solo hacerlo una vez

    public override void Enter()
    {
        Debug.Log("PROGRAMADOR ENTRANDO EN ESTADO DE TRABAJO...");

        WorldManager.Instance.OnNotifyEmployeesEnd += EndMeeting;
        List <IAction> actions = new List<IAction>();
        actions.Add(new AssignReunionChairAction(agent));
        actions.Add(new SitInReunionChairAction(agent));
        _workAction = new CompositeAction(actions);
        _onlyOnce = true;
    }

    public override void Exit()
    {
        WorldManager.Instance.OnNotifyEmployeesEnd -= EndMeeting;
        _onlyOnce = false;
        Debug.Log("PROGRAMADOR HA SALIDO DE ESTADO DE TRABAJO");
    }

    public override void FixedUpdate()
    {
        _workAction?.FixedUpdate();
    }

    public override void Update()
    {
        _workAction.Update();
        if (_onlyOnce && _workAction.Finished) 
        {
            agent.SetBark("Listen");
            agent.SetAnimation("Idle");
            _onlyOnce = false;
        }

    }

    public void EndMeeting()
    {
        if (agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>().GetReunionChair().GetComponent<Chair>().IsOccupied())
        {
            agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>().GetReunionChair().GetComponent<Chair>().Leave();
        }
        agent.SetAgentVariable(agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>().Motivation, 100f);
        context.State = nextState;
    }
}
