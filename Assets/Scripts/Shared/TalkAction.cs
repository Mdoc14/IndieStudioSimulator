using UnityEngine;
using CharactersBehaviour;
using System;

public class TalkAction : ASimpleAction
{
    Func<bool> _condition;
    Action _action;

    public TalkAction(IAgent agent, Func<bool> condition, Action action = null) : base(agent) { _condition = condition; _action = action; }

    public override void Enter()
    {
        base.Enter();
        agent.SetAnimation("Talk");
        agent.SetBark("Report");
        if(_action != null) _action();
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        if (_condition())
        {
            finished = true;
        }
    }
}
