using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitForFoodAction : ASimpleAction
{
    private NavMeshAgent _navAgent;

    public WaitForFoodAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
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
