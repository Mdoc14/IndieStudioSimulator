using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingState : AState
{
    public ReadingState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _readAction;

    public override void Enter()
    {
        Debug.Log("GUINISTA ENTRANDO EN ESTADO DE LEER...");

        List<IAction> actions = new List<IAction>();
        if (!agent.GetChair().IsOccupied())
        {
            actions.Add(new GoToDeskAction(agent));
        }
        actions.Add(new ReadingAction(agent));
        _readAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("GUIONISTA HA SALIDO DE ESTADO DE LEER");
    }

    public override void FixedUpdate()
    {
        _readAction?.FixedUpdate();
    }

    public override void Update()
    {
        _readAction.Update();
        if (_readAction.Finished)
        {
            context.State = new CheckEmployeeNecessitiesState(context, agent, new ScriptWritterWorkState(context, agent));
        }
    }
}
