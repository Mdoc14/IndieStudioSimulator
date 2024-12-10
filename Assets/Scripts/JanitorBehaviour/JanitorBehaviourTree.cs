using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorBehaviourTree : AState
{
    public JanitorBehaviourTree(StateMachine sm, IAgent agent) : base(sm, agent)
    {
    }

    public override void Enter()
    {
        Debug.Log("He entrado al behaviour tree....");
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
