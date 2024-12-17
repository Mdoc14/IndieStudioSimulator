using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SleepAction : ASimpleAction
{
    private float _timeSleeping;
    private bool _reached = false;
    private NavMeshAgent _navAgent;
    CatBehaviour _catBehaviour;
    Chair _catBed;

    public SleepAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _catBehaviour = agent.GetAgentGameObject().GetComponent<CatBehaviour>();
        _reached = false;
        _timeSleeping = Random.Range(60, 120); //Está un tiempo aleatorio durmiendo
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _catBed = _catBehaviour.CatBeds[Random.Range(0, _catBehaviour.CatBeds.Count)].GetComponent<Chair>();
        _navAgent.SetDestination(_catBed.transform.position);
        agent.SetBark("Sleep");
        agent.SetAnimation("Walk");

        Debug.Log("Gato: Va a dormir");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        if (!_reached)  //Si no ha llegado a la cama comprueba si está lo suficientemente cerca para usarla
        {
            if (!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                _reached = true;
                _catBed.Sit(agent.GetAgentGameObject());
                agent.SetAnimation("Sleep");
            }
        }
        else //Si la ha alcanzado el tiempo comienza a descontarse
        {
            _timeSleeping -= Time.deltaTime;
            agent.SetAgentVariable(_catBehaviour.Tiredness, agent.GetAgentVariable(_catBehaviour.Tiredness) - Time.deltaTime, 0, 100);

            if (_timeSleeping <= 0)
            {
                _catBed.Leave();
                finished = true;

                Debug.Log("Gato: Ha terminado de dormir");
            }
        }
    }
}
