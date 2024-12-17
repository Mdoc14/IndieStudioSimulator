using CharactersBehaviour;
using UnityEngine;

public class LookIncidenceAction : ASimpleAction
{
    private float _lookTime = Random.Range(3, 5);

    public LookIncidenceAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        agent.SetBark("Checking");
        agent.SetAnimation("Looking");
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
