using UnityEngine;
using CharactersBehaviour;
using System;

public class TalkAction : ASimpleAction
{
    Func<bool> _condition;
    Action _action;
    bool _initialized = false;
    bool inPlace = false;

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
        if(GameObject.FindWithTag("MaintenanceChair").GetComponent<Chair>().IsOccupied() && !inPlace)
        {
            inPlace = true;
            agent.SetAnimation("Talk");
            agent.SetBark("Report");
        }
        else if(!GameObject.FindWithTag("MaintenanceChair").GetComponent<Chair>().IsOccupied() && inPlace)
        {
            inPlace = false;
            agent.SetAnimation("Idle");
            agent.SetBark("Wait");
        }
        if (_condition())
        {
            finished = true;
        }
    }
}
