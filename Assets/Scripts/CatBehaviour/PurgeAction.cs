using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PurgeAction : ASimpleAction
{
    CatBehaviour _catBehaviour;
    float _timePurging;

    public PurgeAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _catBehaviour = agent.GetAgentGameObject().GetComponent<CatBehaviour>();
        _timePurging = 2.083f;
        agent.SetBark("Vomit");
        agent.SetAnimation("Purge");
        Debug.Log("Gato: se está purgando");
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        _timePurging -= Time.deltaTime;
        if (_timePurging <= 0)
        {
            agent.SetAgentVariable(_catBehaviour.TimeWithoutPurging, 0f);
            WorldManager.Instance.GenerateTrash(agent.GetAgentGameObject().transform.position);
            finished = true;
            Debug.Log("Gato: ha terminado de purgarse");
        }
    }
}