using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : AState
{
    public EatState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _eatAction;
    EmployeeBehaviour _employeeBehaviour;
    bool _alreadySubscribed;

    public override void Enter()
    {
        Debug.Log("PROGRAMADOR ENTRANDO EN ESTADO DE COMER...");
        GameObject vendingMachine = GameObject.Find("VendingMachine");
        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToPositionAction(agent, vendingMachine.transform.position));
        actions.Add(new TakeFoodAction(agent, vendingMachine.GetComponent<VendingMachineManager>()));
        actions.Add(new ProgrammerEatingAction(agent, context));
        _employeeBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
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
            agent.SetAgentVariable(_employeeBehaviour.Motivation, agent.GetAgentVariable(_employeeBehaviour.Motivation) + Random.Range(0, 20f));
            agent.SetAgentVariable(_employeeBehaviour.TimeWithoutConsuming, 0);
            WorldManager.Instance.SetWorkerActivity(false);
            context.State = new CheckEmployeeNecessitiesState(context, agent, new ProgrammerWorkState(context, agent));
        }
    }
}
