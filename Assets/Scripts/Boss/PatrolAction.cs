using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAction : ASimpleAction
{
    BossBehaviour _boss;
    NavMeshAgent _navAgent;

    public PatrolAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Patrullando...");
        _boss = agent.GetAgentGameObject().GetComponent<BossBehaviour>();
        _navAgent = _boss.GetComponent<NavMeshAgent>();
        _navAgent.SetDestination(_boss.GetCurrentWaypoint().position);
        agent.SetBark("Look");
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
