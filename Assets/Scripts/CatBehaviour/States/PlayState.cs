using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : AState
{
    CatBehaviour _catBehaviour;

    IAction _playAction;

    UtilitySystem _us;
    LeafFactor _sociabilityFactor;
    float _currentSociability;
    float _maxSociability;

    GameObject[] _gameObjectsWithTag;
    List<GameObject> _posibleObjetives = new List<GameObject>();

    public PlayState(StateMachine sm, IAgent agent) : base(sm, agent)
    {
    }

    public override void Enter()
    {
        Debug.Log("GATO: HE COMENZADO A JUGAR");
        _catBehaviour = agent.GetAgentGameObject().GetComponent<CatBehaviour>();
        _us = _catBehaviour.US;
        //_sociabilityFactor = _us.GetLeafFactor(_catBehaviour.TimeWithoutSocializing);
        //_currentSociability = _sociabilityFactor.Utility;
        //_maxSociability = _sociabilityFactor.MaxValue;

        float biteWiresProbabilty = Random.Range(0f, 1f);//_currentSociability / _maxSociability;
        float randomNumber = Random.Range(0f, 1f/*_maxSociability*/);

        string selectedTagToPlayWith = null;

        if (randomNumber <= biteWiresProbabilty)
        {
            selectedTagToPlayWith = "Computer";
        }
        else
        {
            selectedTagToPlayWith = "Trashcan";
        }

        _gameObjectsWithTag = GameObject.FindGameObjectsWithTag(selectedTagToPlayWith);
        CheckAvailability(selectedTagToPlayWith);
        _catBehaviour.CurrentObjetive = GetNearObjetive();

        if (_catBehaviour.CurrentObjetive == null)
        {
            Debug.Log("Gato: No había con qué jugar");
            context.State = new WanderingState(context, agent);
            return;
        }

        if (selectedTagToPlayWith.Equals("Computer"))
        {
            agent.SetBark("BiteWires");

            _playAction = new InteractAction(agent, "Bite");
            _playAction.Enter();
        }
        else
        {
            agent.SetBark("ThrowTrash");

            List<IAction> actions = new List<IAction>()
            {
                new InteractAction(agent, "ThrowTrashcan"),
                new RunAwayAction(agent)
            };

            CompositeAction throwTrashCanCompositeAction = new CompositeAction(actions);

            _playAction = throwTrashCanCompositeAction;
        }
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        agent.SetAgentVariable(_catBehaviour.Boredom, agent.GetAgentVariable(_catBehaviour.Boredom) - Time.deltaTime, 0, 100);
        agent.SetAgentVariable(_catBehaviour.Tiredness, agent.GetAgentVariable(_catBehaviour.Tiredness) + Time.deltaTime * 0.8f, 0, 100);

        if (!_playAction.Finished)
        {
            _playAction.Update();
        }
        else
        {
            Debug.Log("GATO: HE TERMINADO DE JUGAR");
            context.State = new WanderingState(context, agent);
        }
    }

    void CheckAvailability(string tag)
    {
        foreach (GameObject go in _gameObjectsWithTag)
        {
            if (tag.Equals("Trashcan"))
            {
                Trashcan trashcan = go.GetComponent<Trashcan>();

                if (!trashcan.Lying)
                {
                    _posibleObjetives.Add(go);
                }
            }
            else
            {
                Computer computer = go.GetComponent<Computer>();

                if (!computer.broken)
                {
                    _posibleObjetives.Add(go);
                }
            }
        }
    }

    GameObject GetNearObjetive()
    {
        GameObject closestObject = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject go in _posibleObjetives)
        {
            float distance = Vector3.Distance(agent.GetAgentGameObject().transform.position, go.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestObject = go;
            }
        }

        return closestObject;
    }
}
