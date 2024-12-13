using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToPositionAction : ASimpleAction
{
    private NavMeshAgent _navAgent;
    private Vector3 _destination;

    public GoToPositionAction(IAgent agent, Vector3 position) : base(agent) { _destination = position; }

    public override void Enter()
    {
        base.Enter();
        if (agent.GetChair().IsOccupied()) agent.GetChair().Leave();
        if (agent.GetCurrentChair() != null && agent.GetCurrentChair().IsOccupied()) agent.GetCurrentChair().Leave();
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _navAgent.SetDestination(_destination);
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
        if(_navAgent.remainingDistance <= _navAgent.stoppingDistance && !_navAgent.pathPending) 
        {
            finished = true;
        }
    }
}
