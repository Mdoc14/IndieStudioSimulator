using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrinkingAction : ASimpleAction
{
    private NavMeshAgent _navAgent;
    StateMachine _context;

    float _time;

    public DrinkingAction(IAgent agent, StateMachine sm) : base(agent) { _context = sm; }

    public override void Enter()
    {
        base.Enter();
        _time = Random.Range(5, 15);
        agent.SetBark("Drink");
        agent.SetAnimation("Drink");
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
        agent.SetAgentVariable((agent as EmployeeBehaviour).Motivation, agent.GetAgentVariable((agent as EmployeeBehaviour).Motivation) + Time.deltaTime);
        if (_time <= 0)
        {
            finished = true;
        }
    }
}
