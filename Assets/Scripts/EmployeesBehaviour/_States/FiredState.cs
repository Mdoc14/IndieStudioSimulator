using CharactersBehaviour;
using UnityEngine;

public class FiredState : AState
{
    ASimpleAction _firedAction;
    bool _reached = false;
    public FiredState(StateMachine sm, IAgent agent) : base(sm, agent)
    {
    }

    public override void Enter()
    {
        Debug.Log("HA SIDO DESPEDIDO");
        if (agent.GetChair().IsOccupied()) agent.GetChair().Leave();
        if (agent.GetCurrentChair() != null && agent.GetCurrentChair().IsOccupied()) agent.GetCurrentChair().Leave();
        _firedAction = new GoToPositionAction(agent, GameObject.FindWithTag("FiredPosition").transform.position);
        _firedAction.Enter();
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        _firedAction?.FixedUpdate();
    }

    public override void Update()
    {
        if(!_firedAction.Finished) _firedAction.Update();
        if(_firedAction.Finished)
        {
            if (!_reached)
            {
                _reached = true;
                agent.SetBark("Scolded");
                agent.SetAnimation("Idle");
                GameObject.FindWithTag("Elevator").GetComponent<Animator>().SetTrigger("Close");
                (agent as EmployeeBehaviour).FireEmployee(1);
            }
        }
    }
}
