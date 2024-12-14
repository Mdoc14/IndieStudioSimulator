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
        Debug.Log("GATO: COMPROBANDO NECESIDADES");
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
            Debug.Log("GATO: NECESIDAD COMPLETADA");
            context.State = new WanderingState(context, agent);
        }
    }
}
