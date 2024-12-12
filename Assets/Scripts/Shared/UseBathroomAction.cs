using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UseBathroomAction : ASimpleAction
{
    private float _time;
    private bool _reached = false;
    private NavMeshAgent _navAgent;
    Chair _bath;
    bool keepBathroom;

    public UseBathroomAction(IAgent agent, bool keepBathroom = false) : base(agent) 
    { 
        this.keepBathroom = keepBathroom;
    }

    public override void Enter()
    {
        base.Enter();
        _time = Random.Range(3, 60); //Está un tiempo aleatorio usando el baño
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _bath = agent.GetBath();
        _navAgent.SetDestination(_bath.transform.position);
        //agent.SetBark("Bathroom");
        //agent.SetAnimation("Walk");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        if (!_reached)  //Si no ha llegado a él comprueba si está lo suficientemente cerca para usarlo
        {
            if (!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                _reached = true;
                _bath.Sit(agent.GetAgentGameObject());
                //agent.SetAnimation("Idle");
            } 
        }
        else //Si lo ha alcanzado el tiempo comienza a descontarse
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                _bath.Leave();
                if (!keepBathroom) agent.SetBath(null);
                finished = true;
            }
        }
    }
}
