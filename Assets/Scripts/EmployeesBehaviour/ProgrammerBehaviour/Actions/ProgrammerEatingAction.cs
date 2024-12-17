using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProgrammerEatingAction : ASimpleAction
{
    private NavMeshAgent _navAgent;
    StateMachine _context;
    EmployeeBehaviour _programmerBehaviour;

    float _time;

    public ProgrammerEatingAction(IAgent agent, StateMachine sm) : base(agent) { _context = sm; }

    public override void Enter()
    {
        base.Enter();
        _time = Random.Range(5, 15);
        _programmerBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        agent.SetBark("Eat");
        agent.SetAnimation("Eat");
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
        if (_time <=0)
        {
            finished = true;
            WorldManager.Instance.GenerateTrash(agent.GetAgentGameObject().transform.position);
            agent.SetAgentVariable(_programmerBehaviour.TimeWithoutConsuming, 0f);
            agent.SetAgentVariable(_programmerBehaviour.Motivation, agent.GetAgentVariable(_programmerBehaviour.Motivation) + Random.Range(0f, 20f));
        }
    }
}
