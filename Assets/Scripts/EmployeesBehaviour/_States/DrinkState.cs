using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class DrinkState : AState
{
    public DrinkState(StateMachine sm, IAgent agent) : base(sm, agent) { _stateMachine = sm; }
    CompositeAction _drinkAction;
    EmployeeBehaviour _employeeBehaviour;
    StateMachine _stateMachine;

    public override void Enter()
    {
        Debug.Log("TRABAJADOR ENTRANDO EN ESTADO DE BEBER...");
        GameObject vendingMachine = GameObject.Find("VendingMachine");
        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToPositionAction(agent, vendingMachine.transform.position));
        actions.Add(new TakeDrinkAction(agent, vendingMachine.GetComponent<VendingMachineManager>(), _stateMachine));
        actions.Add(new DrinkingAction(agent, context));
        _employeeBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        _drinkAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("TRABAJADOR HA SALIDO DE ESTADO DE BEBER");
    }

    public override void FixedUpdate()
    {
        _drinkAction?.FixedUpdate();
    }

    public override void Update()
    {
        _drinkAction?.Update();
        if (_drinkAction.Finished)
        {
            agent.SetAgentVariable((agent as EmployeeBehaviour).TimeWithoutConsuming, 0);
            WorldManager.Instance.SetWorkerActivity(false);
            if (_employeeBehaviour is ArtistBehaviour)
            {
                context.State = new CheckEmployeeNecessitiesState(context, agent, new ArtistWorkState(context, agent));
            }
            else if (_employeeBehaviour is ScriptWritterBehaviour) 
            {
                context.State = new CheckEmployeeNecessitiesState(context, agent, new ScriptWritterWorkState(context, agent));
            }
        }
    }
}
