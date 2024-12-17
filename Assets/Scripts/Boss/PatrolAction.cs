using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAction : ASimpleAction
{
    BossBehaviour _boss;
    NavMeshAgent _navAgent;
    float _initSpeed;

    public PatrolAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Patrullando...");
        _boss = agent.GetAgentGameObject().GetComponent<BossBehaviour>();
        _navAgent = _boss.GetComponent<NavMeshAgent>();
        _initSpeed = _navAgent.speed;
        _navAgent.speed = 1f;
        _navAgent.SetDestination(_boss.GetCurrentWaypoint().position);
        agent.SetBark("Look");
        agent.SetAnimation("Patrol");
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
            _navAgent.SetDestination(_boss.GetNextWaypoint().position);
        }
    }
}
