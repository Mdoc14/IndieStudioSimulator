using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitingBathroomAction : ASimpleAction
{
    public WaitingBathroomAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        agent.SetBark("Bathroom");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        Chair bath = WorldManager.Instance.GetBathroom(agent);
        if(bath != null)
        {
            finished = true;
        }
    }
}
