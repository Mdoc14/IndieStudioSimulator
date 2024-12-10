using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToDeskAction : ASimpleAction
{
    NavMeshAgent _navAgent;
    Chair _agentChair;

    public GoToDeskAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Yendo al despacho...");
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _agentChair = agent.GetChair();
        _navAgent.SetDestination(_agentChair.transform.position);
    }

    public override void Exit()
    {
        Debug.Log("Despacho alcanzado");
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if(!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
        {
            _agentChair.Sit(agent.GetAgentGameObject());
            finished = true;
        }
    }
}
