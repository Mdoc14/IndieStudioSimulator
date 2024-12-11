using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AState
{
    float currentTime;
    float timeResting;
    CatBehaviour catBehaviour;

    public IdleState(StateMachine sm, IAgent agent) : base(sm, agent)
    {
    }

    public override void Enter()
    {
        timeResting = Random.Range(30f, 120f);
        catBehaviour = agent.GetAgentGameObject().GetComponent<CatBehaviour>();
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        agent.SetAgentVariable(catBehaviour.Boredom, agent.GetAgentVariable(catBehaviour.Boredom) + Time.deltaTime);

        currentTime += Time.deltaTime;

        if (currentTime > timeResting)
        {
            context.State = new WanderingState(context, agent);
        }
    }
}
