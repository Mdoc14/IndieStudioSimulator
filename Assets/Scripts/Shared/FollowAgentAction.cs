using CharactersBehaviour;
using System;
using UnityEngine.AI;

public class FollowAgentAction : ASimpleAction
{
    IAgent _targetAgent;
    NavMeshAgent _navAgent;
    float _prevStoppingDistance;
    Func<bool> _condition;

    public FollowAgentAction(IAgent agent, IAgent target, Func<bool> condition) : base(agent) 
    { 
        _targetAgent = target; 
        _condition = condition; 
    }

    public override void Enter()
    {
        base.Enter();
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _prevStoppingDistance = _navAgent.stoppingDistance;
        _navAgent.stoppingDistance = 1.5f;
        if (agent.GetChair().IsOccupied()) agent.GetChair().Leave();
        if (agent.GetCurrentChair().IsOccupied())
        {
            agent.GetCurrentChair().Leave();
            agent.SetCurrentChair(null);
        }
        agent.SetAnimation("Walk");
        agent.SetBark("Wait");
    }

    public override void Exit()
    {
        _navAgent.stoppingDistance = _prevStoppingDistance;
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        _navAgent.SetDestination(_targetAgent.GetAgentGameObject().transform.position);
        if (_condition()) finished = true;
    }
}
