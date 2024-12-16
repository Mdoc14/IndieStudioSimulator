using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeState : AState
{
    public SmokeState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _smokeAction;
    EmployeeBehaviour _employeeBehaviour;
    GameObject _smokePos;

    public override void Enter()
    {
        Debug.Log("PROGRAMADOR ENTRANDO EN ESTADO DE FUMAR...");

        _employeeBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        SelectSmokePos();
        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToPositionAction(agent, _smokePos.transform.position));
        actions.Add(new SmokingAction(agent, _smokePos));
        _smokeAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("PROGRAMADOR HA SALIDO DE ESTADO DE FUMAR");
        _smokePos.GetComponent<SmokePos>().IsSelected = false;
    }

    public override void FixedUpdate()
    {
        _smokeAction?.FixedUpdate();
    }

    public override void Update()
    {
        _smokeAction?.Update();
        if (_smokeAction.Finished)
        {
            context.State = new CheckEmployeeNecessitiesState(context, agent, new ProgrammerWorkState(context, agent));
        }
    }
    public void SelectSmokePos()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("SmokeArea"))
        {
            if (!obj.GetComponent<SmokePos>().IsSelected)
            {
                _smokePos = obj;
                obj.GetComponent<SmokePos>().IsSelected = true;
                return;
            }
        }
    }
}
