using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

internal class InteractAction : ASimpleAction
{
    CatBehaviour _catBehaviour;
    NavMeshAgent _navAgent;
    IInteractable _interactable;
    bool _reached;
    float _timePlaying;

    public InteractAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _catBehaviour = agent.GetAgentGameObject().GetComponent<CatBehaviour>();
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _timePlaying = Random.Range(5f, 10f);
        _interactable = _catBehaviour.CurrentObjetive.GetComponent<IInteractable>();
        _reached = false;
        _navAgent.SetDestination(_catBehaviour.CurrentObjetive.transform.position);
        Debug.Log("Gato: va a jugar");
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
            _timePlaying -= Time.deltaTime;

            if (_timePlaying <= 0)
            {
                _interactable.Interact();
                finished = true;
                Debug.Log("Gato: termina de jugar");
            }
        }
    }
}