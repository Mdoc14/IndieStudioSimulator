using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePhotoState : AState
{
    public TakePhotoState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    CompositeAction _takePhotoAction;

    public override void Enter()
    {
        Debug.Log("ARTISTA ENTRANDO EN ESTADO DE SACAR FOTOS...");
        (agent as EmployeeBehaviour).isSlacking = true;

        List<IAction> actions = new List<IAction>();
        if (!agent.GetChair().IsOccupied())
        {
            actions.Add(new GoToDeskAction(agent));
        }
        actions.Add(new TakePhotoAction(agent));
        _takePhotoAction = new CompositeAction(actions);
    }

    public override void Exit()
    {
        Debug.Log("ARTISTA HA SALIDO DE ESTADO DE SACAR FOTOS");
        (agent as EmployeeBehaviour).isSlacking = false;
    }

    public override void FixedUpdate()
    {
        _takePhotoAction?.FixedUpdate();
    }

    public override void Update()
    {
        _takePhotoAction.Update();
        if (_takePhotoAction.Finished)
        {
            context.State = new CheckEmployeeNecessitiesState(context, agent, new ArtistWorkState(context, agent));
        }
    }

}
