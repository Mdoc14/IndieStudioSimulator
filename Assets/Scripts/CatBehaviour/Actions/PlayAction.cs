using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayAction : ASimpleAction
{
    StateMachine _sm;
    public PlayAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Gato: Le apetece jugar");
        _sm = agent.GetAgentGameObject().GetComponent<CatBehaviour>().SM;
        _sm.State = new PlayState(_sm, agent);
        finished = true;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
    }
}