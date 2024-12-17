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
        Debug.Log("Gato: Ha entrado en el estado de deambular");
        agent.SetBark("Walk");
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
        agent.SetAgentVariable(catBehaviour.Boredom, agent.GetAgentVariable(catBehaviour.Boredom) + Time.deltaTime, 0, 100);
        agent.SetAgentVariable(catBehaviour.Tiredness, agent.GetAgentVariable(catBehaviour.Tiredness) + Time.deltaTime * 0.5f, 0, 100);

        if (!wanderAction.Finished)
        {
            wanderAction.Update();
        }
        else
        {
            Debug.Log("Gato: Ha salido del estado de deambular");
            context.State = new CheckNecessitiesState(context, agent);
        }
    }
}
