using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReunionState : AState
{
    public BossReunionState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _reunionAction;
    List<IAction> actions = new List<IAction>();

    public override void Enter()
    {
        Debug.Log("EL JEFE VA A EFECTUAR UNA REUNIÓN...");
        context.PreviousStates.Push(this);
        if(actions.Count > 0)
        {
            _reunionAction.CurrentAction.Enter();
            return;
        }
        actions.Add(new GoToPositionAction(agent, GameObject.FindWithTag("BossReunionChair").transform.position));
        actions.Add(new NotifyReunionAction(agent));
        actions.Add(new PerformReunionAction(agent));
        _reunionAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("ESTADO DE LA REUNIÓN FINALIZADO");
    }

    public override void FixedUpdate()
    {
        _reunionAction?.FixedUpdate();
    }

    public override void Update()
    {
        _reunionAction?.Update();
        if (_reunionAction.Finished)
        {
            context.State = new BossWorkState(context, agent);
        }
    }
}
