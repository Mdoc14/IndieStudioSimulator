using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoEatFoodAction : ASimpleAction
{
    private NavMeshAgent _navAgent;
    GameObject _breakRoom;

    public GoEatFoodAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        _breakRoom = GameObject.Find("");
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _navAgent.SetDestination(_breakRoom.transform.position);
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
        if (_navAgent.remainingDistance <= _navAgent.stoppingDistance && !_navAgent.pathPending)
        {
            finished = true;
        }
    }
}
