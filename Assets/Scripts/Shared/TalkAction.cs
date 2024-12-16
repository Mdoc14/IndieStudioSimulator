using UnityEngine;
using CharactersBehaviour;
using System;

public class TalkAction : ASimpleAction
{
    Func<bool> _condition;
    Action _action;
    bool _initialized = false;

    public TalkAction(IAgent agent, Func<bool> condition, Action action = null) : base(agent) { _condition = condition; _action = action; }

    public override void Enter()
    {
        base.Enter();
        if (_initialized) //Si el de mantenimiento se ha tenido que levantar y la acción estaba empezada se ha de esperar a que vuelva para seguir hablando
        {
            agent.SetAnimation("Idle");
            agent.SetBark("Wait");
        }
        else
        {
            agent.SetAnimation("Talk");
            agent.SetBark("Report");
            GameObject.FindWithTag("MaintenanceChair").GetComponent<Chair>().OnSit += StartTalking;
        }
        if(_action != null) _action();
        _initialized = true;
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

    private void StartTalking()
    {
        agent.SetAnimation("Talk");
        agent.SetBark("Report");
    }
}
