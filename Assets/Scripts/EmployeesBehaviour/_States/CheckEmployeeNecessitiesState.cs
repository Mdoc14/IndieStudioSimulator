using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEmployeeNecessitiesState : AState
{
    EmployeeBehaviour _employeeBehaviour;
    AState _employeeLastState;

    public CheckEmployeeNecessitiesState(StateMachine sm, IAgent agent, AState employeeLastState) : base(sm, agent)
    {
        _employeeLastState = employeeLastState;
    }

    public override void Enter()
    {
        _employeeBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        _employeeBehaviour.WorkerUS.activated = true;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        if (_employeeBehaviour.WorkerUS.CurrentAction == null)
        {
            context.State = _employeeLastState;
        }
    }
}
