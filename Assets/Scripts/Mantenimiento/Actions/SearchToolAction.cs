using CharactersBehaviour;
using UnityEngine;

public class SearchToolAction : ASimpleAction
{
    private float _searchTime = Random.Range(5, 10);

    public SearchToolAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        agent.SetBark("Take");
        agent.SetAnimation("Repair");
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        _searchTime -= Time.deltaTime;
        if (_searchTime <= 0)
        {
            finished = true;
        }
    }
}
