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
        Debug.Log("ARTISTA ENTRANDO EN ESTADO DE MIRAR POR LA VENTANA...");

        SelectLookOutsidePos();
        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToPositionAction(agent, _lookOutsidePos.transform.position));
        actions.Add(new LookOutsideAction(agent, _lookOutsidePos));
        _lookOutsideAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("ARTISTA HA SALIDO DE ESTADO DE MIRAR POR LA VENTANA");
        _lookOutsidePos.GetComponent<LookOutsidePos>().IsSelected = false;
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
