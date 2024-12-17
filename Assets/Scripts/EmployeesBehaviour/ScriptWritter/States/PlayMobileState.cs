using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMobileState : AState
{
    public PlayMobileState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _playMobileAction;

    public override void Enter()
    {
        Debug.Log("GUIONISTA ENTRANDO EN ESTADO DE JUGAR AL MÓVIL...");
        (agent as EmployeeBehaviour).isSlacking = true;

        List<IAction> actions = new List<IAction>();
        if (!agent.GetChair().IsOccupied())
        {
            actions.Add(new GoToDeskAction(agent));
        }
        actions.Add(new PlayMobileAction(agent));
        _playMobileAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("`GUIONISTA HA SALIDO DE ESTADO DE JUGAR AL MÓVIL");
        (agent as EmployeeBehaviour).isSlacking = false;
    }

    public override void FixedUpdate()
    {
        _playMobileAction?.FixedUpdate();
    }

    public override void Update()
    {
        _playMobileAction.Update();
        if (_playMobileAction.Finished)
        {
            context.State = new CheckEmployeeNecessitiesState(context, agent, new ScriptWritterWorkState(context, agent));
        }
    }

}
