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
    bool _cantConsume = false;
    bool _animationChanged = false;
    StateMachine _stateMachine;

    public TakeFoodAction(IAgent agent, VendingMachineManager vendingMachine, StateMachine sm) : base(agent)
    {
        _machineManager = vendingMachine;
        _stateMachine = sm;
    }
    public override void Enter()
    {
        base.Enter();
        if (!_machineManager.IsEmpty())
        {
            _machineManager.TakeProduct();
            agent.SetBark("Take");
            agent.SetAnimation("Take");
        }
        else
        {
            _time = 6f;
            _cantConsume = true;
            agent.SetBark("Checking");
            agent.SetAnimation("Idle");
            agent.SetAgentVariable((agent as EmployeeBehaviour).Motivation, Mathf.Clamp(agent.GetAgentVariable((agent as EmployeeBehaviour).Motivation) - Random.Range(10f, 20f), 0, 100));
            agent.SetAgentVariable((agent as EmployeeBehaviour).TimeWithoutConsuming, 0);
        }
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        if (!_cantConsume)
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                finished = true;
            }
        }
        else
        {
            _time -= Time.deltaTime;
            if (_time <= 3 && !_animationChanged)
            {
                _animationChanged = true;
                agent.SetBark("Scolded");
                agent.SetAnimation("Scolded");
            }
            if (_time <= 0)
            {
                _stateMachine.State = new CheckEmployeeNecessitiesState(_stateMachine, agent, new ProgrammerWorkState(_stateMachine, agent));
                finished = true;
            }
        }
    }
}
