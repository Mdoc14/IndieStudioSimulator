using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateAction : ASimpleAction
{
    StateMachine _context;
    AState _state;
    public ChangeStateAction(StateMachine sm,IAgent agent, AState USState) : base(agent)
    {
        _state = USState;
        _context = sm;
    }
    public override void Enter()
    {
        base.Enter();
        _context.State = _state;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        finished = true;
    }
}
