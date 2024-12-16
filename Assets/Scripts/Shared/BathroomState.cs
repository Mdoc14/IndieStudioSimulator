using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomState : AState
{
    public BathroomState(StateMachine sm, IAgent agent, AState nextState, Action action = null) : base(sm, agent) 
    {
        this.nextState = nextState;
        _action = action;
    }
    Action _action;
    AState nextState;
    List<IAction> actions = new List<IAction>();
    CompositeAction _bathroomAction;
    GameObject _currentWaitingLine;

    public override void Enter()
    {
        //El estado de ir al baño se divide en tres acciones: ir al baño, comprobar si hay un retrete libre y usarlo
        Debug.Log("HE ENTRADO EN EL ESTADO DE IR AL BAÑO");
        context.PreviousStates.Push(this);
        SelectBathroom();
        if (actions.Count > 0 && !_bathroomAction.Finished && actions[0].Finished)
        {
            _bathroomAction.CurrentAction.Enter();
            return; //Si ya está inicializado el estado que no se cree la lista de nuevo
        }
        actions.Clear();
        actions.Add(new GoToPositionAction(agent, _currentWaitingLine.transform.position));
        actions.Add(new WaitingBathroomAction(agent, _currentWaitingLine.GetComponent<BathroomWaitingLine>()));
        actions.Add(new UseBathroomAction(agent, context, _action));
        _bathroomAction = new CompositeAction(actions);
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        _bathroomAction?.FixedUpdate();
    }

    public override void Update()
    {
        _bathroomAction?.Update();
        if (_bathroomAction.Finished)
        {
            context.State = nextState;
        }
    }

    private void SelectBathroom()
    {
        foreach(GameObject line in GameObject.FindGameObjectsWithTag("BathroomWaitingLine"))
        {
            if (line.GetComponent<BathroomWaitingLine>().maleBathroom == (agent as AgentBehaviour).male) _currentWaitingLine = line;
        }
    }
}
