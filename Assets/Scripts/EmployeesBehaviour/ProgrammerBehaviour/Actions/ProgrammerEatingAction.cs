using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProgrammerEatingAction : ASimpleAction
{
    private NavMeshAgent _navAgent;
    StateMachine context;

    public ProgrammerEatingAction(IAgent agent, StateMachine sm) : base(agent) { context = sm; }

    public override void Enter()
    {
        base.Enter();
        agent.SetBark("Eat");
        agent.SetAnimation("Eat");
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
            context.State = new ProgrammerWorkState(context, agent);
        }
    }
}
