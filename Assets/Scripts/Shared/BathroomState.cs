using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomState : AState
{
    public BathroomState(StateMachine sm, IAgent agent, AState nextState) : base(sm, agent) 
    {
        this.nextState = nextState;
    }
    AState nextState;
    CompositeAction _bathroomAction;
    GameObject _currentWaitingLine;

    public override void Enter()
    {
        //El estado de ir al baño se divide en tres acciones: ir al baño, comprobar si hay un retrete libre y usarlo
        SelectBathroom();
        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToPositionAction(agent, _currentWaitingLine.transform.position));
        actions.Add(new WaitingBathroomAction(agent, _currentWaitingLine.GetComponent<BathroomWaitingLine>()));
        actions.Add(new UseBathroomAction(agent));
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
