using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToAction : ASimpleAction
{
    private NavMeshAgent _navAgent;
    Func<Vector3> destination;
    Room currentRoomn;

    public GoToAction(IAgent agent, Func<Vector3> destinationInRoom) : base(agent)
    {
        destination = destinationInRoom;
    }

    public override void Enter()
    {
        base.Enter();
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _navAgent.SetDestination(destination.Invoke());
    }
    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        if (_navAgent.remainingDistance <= _navAgent.stoppingDistance && !_navAgent.pathPending)
        {
            finished = true;
        }
    }

    void Start()
    {
        
    }
}
