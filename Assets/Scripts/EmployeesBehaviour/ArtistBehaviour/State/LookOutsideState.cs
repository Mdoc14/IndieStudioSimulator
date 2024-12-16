using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOutsideState : AState
{
    public LookOutsideState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _lookOutsideAction;
    GameObject _lookOutsidePos;

    public override void Enter()
    {
        Debug.Log("PROGRAMADOR ENTRANDO EN ESTADO DE FUMAR...");

        SelectLookOutsidePos();
        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToPositionAction(agent, _lookOutsidePos.transform.position));
        actions.Add(new SmokingAction(agent, _lookOutsidePos));
        _lookOutsideAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("PROGRAMADOR HA SALIDO DE ESTADO DE FUMAR");
        _lookOutsidePos.GetComponent<SmokePos>().IsSelected = false;
    }

    public override void FixedUpdate()
    {
        _lookOutsideAction?.FixedUpdate();
    }

    public override void Update()
    {
        _lookOutsideAction?.Update();
        if (_lookOutsideAction.Finished)
        {
            context.State = new CheckEmployeeNecessitiesState(context, agent, new ArtistWorkState(context, agent));
        }
    }
    public void SelectLookOutsidePos()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("LookOutsidePos"))
        {
            if (!obj.GetComponent<LookOutsidePos>().IsSelected)
            {
                _lookOutsidePos = obj;
                obj.GetComponent<LookOutsidePos>().IsSelected = true;
                return;
            }
        }
    }
}
