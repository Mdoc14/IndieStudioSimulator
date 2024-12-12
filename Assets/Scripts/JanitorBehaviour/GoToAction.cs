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
    Room currentRoom;
    string barkName;

    //Variable temporales
    float timer;

    public GoToAction(IAgent agent, Func<Vector3> destinationInRoom, string bark) : base(agent)
    {
        destination = destinationInRoom;
        barkName = bark;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Accion: caminar");
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        Vector3 destiny = destination.Invoke();

        _navAgent.isStopped = false;
        _navAgent.SetDestination(destiny);

        agent.SetBark(barkName);
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
            if (barkName == "Walk") 
            {
                Debug.Log("");
            }
            Debug.Log("He llegado a mi destino");
            finished = true;
            _navAgent.isStopped = true;
        }
    }

    void Start()
    {
        
    }
}
