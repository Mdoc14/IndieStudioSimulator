using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatAction : ASimpleAction
{
    CatBehaviour _catBehaviour;
    NavMeshAgent _navAgent;
    GameObject _catBowl;
    bool _reached;
    float _timeEating;

    public EatAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _catBehaviour = agent.GetAgentGameObject().GetComponent<CatBehaviour>();
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _timeEating = Random.Range(60f, 120f);
        _catBowl = _catBehaviour.CatBowls[Random.Range(0, 1)];
        _reached = false;
        _navAgent.SetDestination(_catBowl.transform.position);
        agent.SetBark("Eat");
        Debug.Log("Gato: va a comer");
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        if (!_reached)
        {
            if (!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                _reached = true;
            }
        }
        else
        {
            _timeEating -= Time.deltaTime;
            agent.SetAgentVariable(_catBehaviour.Tiredness, agent.GetAgentVariable(_catBehaviour.Tiredness) - Time.deltaTime);

            if (_timeEating <= 0)
            {
                agent.SetAgentVariable(_catBehaviour.TimeWithoutEating, 0f);
                finished = true;
                Debug.Log("Gato: termina de comer");
            }
        }
    }
}