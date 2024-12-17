using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : AState
{
    public EatState(StateMachine sm, IAgent agent) : base(sm, agent) { _stateMachine = sm; }
    CompositeAction _eatAction;
    StateMachine _stateMachine;
    bool _alreadySubscribed;

    public override void Enter()
    {
        Debug.Log("PROGRAMADOR ENTRANDO EN ESTADO DE COMER...");
        GameObject vendingMachine = GameObject.Find("VendingMachine");
        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToPositionAction(agent, vendingMachine.transform.position));
        actions.Add(new TakeFoodAction(agent, vendingMachine.GetComponent<VendingMachineManager>(), _stateMachine));
        actions.Add(new ProgrammerEatingAction(agent, context));
        _eatAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("PROGRAMADOR HA SALIDO DE ESTADO DE COMER");
    }

    public override void FixedUpdate()
    {
        _eatAction?.FixedUpdate();
    }

    public override void Update()
    {
        _eatAction?.Update();
        if (_eatAction.Finished)
        {
            context.State = new CheckEmployeeNecessitiesState(context, agent, new ProgrammerWorkState(context, agent));
        }
    }
}
