using CharactersBehaviour;
using UnityEngine;

public class RepairAction : ASimpleAction
{
    private float _repairTime = Random.Range(7, 15);

    public RepairAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        agent.SetBark("Repair");
        agent.SetAnimation("Repair");
    }

    public override void Exit()
    {
        (agent as MaintenanceBehaviour).GetCurrentIncidence().Repair();
        (agent as MaintenanceBehaviour).SetCurrentIncidence(null);
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        _repairTime -= Time.deltaTime;
        if (_repairTime <= 0)
        {
            finished = true;
        }
    }
}
