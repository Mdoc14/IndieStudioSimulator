using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UseLitterBoxAction : ASimpleAction
{
    private float _time;
    private bool _reached = false;
    private NavMeshAgent _navAgent;
    Chair _bath;
    CatBehaviour _catBehaviour;

    public UseLitterBoxAction(IAgent agent) : base(agent) 
    { 
    }

    public override void Enter()
    {
        base.Enter();
        _catBehaviour = agent.GetAgentGameObject().GetComponent<CatBehaviour>();
        _reached = false;
        _time = Random.Range(10, 30); //Está un tiempo aleatorio usando el baño
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _bath = agent.GetCurrentChair();
        _navAgent.SetDestination(_bath.transform.position);
        agent.SetBark("Bathroom");
        agent.SetAnimation("Walk");
        Debug.Log("Gato: Ha ido a hacer sus necesidades");
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
                agent.SetAnimation("Bath");
            } 
        }
        else //Si lo ha alcanzado el tiempo comienza a descontarse
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                _bath.Leave();
                _bath.gameObject.GetComponent<CatBoxManager>().SetDirty();
                agent.SetAgentVariable(_catBehaviour.TimeWithoutBath, 0f);
                finished = true;
                Debug.Log("Gato: Ha terminado de hacer sus necesidades");
            }
        }
    }
}
