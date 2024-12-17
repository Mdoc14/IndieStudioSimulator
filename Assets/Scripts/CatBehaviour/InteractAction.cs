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
    string _type;

    public InteractAction(IAgent agent, string animationType) : base(agent)
    {
        _type = animationType;
    }

    public override void Enter()
    {
        base.Enter();
        agent.SetAnimation("Walk");
        _catBehaviour = agent.GetAgentGameObject().GetComponent<CatBehaviour>();
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _timePlaying = Random.Range(1.5f, 2f);
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
                agent.SetAnimation(_type);
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