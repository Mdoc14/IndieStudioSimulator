using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingState : AState
{
    CatBehaviour catBehaviour;
    WanderAction wanderAction;

    public WanderingState(StateMachine sm, IAgent agent) : base(sm, agent)
    {
    }

    public override void Enter()
    {
        Debug.Log("GATO: HE COMENZADO A DEAMBULAR");
        catBehaviour = agent.GetAgentGameObject().GetComponent<CatBehaviour>();
        wanderAction = new WanderAction(agent);
        wanderAction.Enter();
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
        agent.SetAgentVariable(catBehaviour.Tiredness, agent.GetAgentVariable(catBehaviour.Tiredness) + Time.deltaTime);

        if (!wanderAction.HasFinished)
        {
            wanderAction.Update();
        }
        else
        {
            Debug.Log("GATO: HE TERMINADO DE DEAMBULAR");
            context.State = new CheckNecessitiesState(context, agent);
        }
    }
}
