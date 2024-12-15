using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitingBathroomAction : ASimpleAction
{
    BathroomWaitingLine _currentLine;
    public WaitingBathroomAction(IAgent agent, BathroomWaitingLine waitingLine = null) : base(agent) { _currentLine = waitingLine; }

    public override void Enter()
    {
        base.Enter();
        agent.SetBark("Wait");
        agent.SetAnimation("Idle");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        Chair bath = _currentLine.GetBathroom(agent);
        if(bath != null)
        {
            finished = true;
        }
    }
}
