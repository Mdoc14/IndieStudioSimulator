using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToDeskAction : ASimpleAction
{
    NavMeshAgent _navAgent;
    Chair _agentChair;

    public GoToDeskAction(IAgent agent, Chair chair = null) : base(agent) { _agentChair = chair; }

    public override void Enter()
    {
        base.Enter();
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        if(_agentChair == null) _agentChair = agent.GetChair();
        _navAgent.SetDestination(_agentChair.transform.position);
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
        if(!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
        {
            _agentChair.Sit(agent.GetAgentGameObject());
            if(_agentChair != agent.GetChair()) agent.SetCurrentChair(_agentChair);
            finished = true;
        }
    }
}
