using UnityEngine;
using CharactersBehaviour;

public class ListenIncidenceAction : ASimpleAction
{
    float _time;
    public ListenIncidenceAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        agent.SetAnimation("Idle");
        agent.SetBark("Listen");
        _time = Random.Range(3, 10);
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            finished = true;
        }
    }
}
