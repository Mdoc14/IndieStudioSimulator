using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TakeFoodAction : ASimpleAction
{
    private NavMeshAgent _navAgent;
    private VendingMachineManager _machineManager;
    float _time = 3f;

    public TakeFoodAction(IAgent agent, VendingMachineManager vendingMachine) : base(agent) { _machineManager = vendingMachine; }

    public override void Enter()
    {
        base.Enter();
        _machineManager.TakeProduct();
        agent.SetBark("Take");
        agent.SetAnimation("Take");
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
