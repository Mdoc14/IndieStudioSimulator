using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNecessitiesState : AState
{
    CatBehaviour catBehaviour;

    public CheckNecessitiesState(StateMachine sm, IAgent agent) : base(sm, agent)
    {
    }

    public override void Enter()
    {
        Debug.Log("Gato: Ha entrado en el estado de comprobar necesidades");
        catBehaviour = agent.GetAgentGameObject().GetComponent<CatBehaviour>();
        catBehaviour.US.activated = true;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        if (catBehaviour.US.CurrentAction == null)
        {
            Debug.Log("Gato: Ha salido del estado de comprobar necesidades");
            context.State = new WanderingState(context, agent);
        }
    }
}
