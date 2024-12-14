using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SitInChairAction : ASimpleAction
{
    NavMeshAgent _navAgent;
    Chair _reunionChair;

    public SitInChairAction(IAgent agent) : base(agent) {}

    public override void Enter()
    {
        base.Enter();
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        if (_reunionChair == null) _reunionChair = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>().GetReunionChair().GetComponent<Chair>();
        _navAgent.SetDestination(_reunionChair.transform.position);
        agent.SetBark("Walk");
        agent.SetAnimation("Walk");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        if (!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
        {
            _reunionChair.Sit(agent.GetAgentGameObject());
            finished = true;
        }
    }
}

