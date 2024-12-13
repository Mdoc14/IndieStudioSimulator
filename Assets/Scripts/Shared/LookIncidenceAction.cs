using CharactersBehaviour;
using UnityEngine;

public class LookIncidenceAction : ASimpleAction
{
    private float _lookTime = 3;

    public LookIncidenceAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        agent.SetBark("Empty");
        agent.SetAnimation("Look");
    }

    public override void Exit()
    { 
        if(agent.GetChair().IsOccupied()) agent.GetChair().Leave();
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        _lookTime -= Time.deltaTime;
        if (_lookTime <= 0)
        {
            finished = true;
        }
    }
}
