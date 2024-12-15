using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAction : ASimpleAction
{
    private float _smokeTime;
    private EmployeeBehaviour _programmerBehaviour;
    public SmokeAction(IAgent agent) : base(agent) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Programador está fumando...");
        _smokeTime = Random.Range(5, 15);
        _programmerBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        agent.SetBark("Smoke");
        agent.SetAnimation("Smoke");
    }
    public override void Exit()
    {
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        _smokeTime -= Time.deltaTime;
        agent.SetAgentVariable(_programmerBehaviour.TimeWithoutBath, agent.GetAgentVariable(_programmerBehaviour.TimeWithoutBath + Time.deltaTime));
        agent.SetAgentVariable(_programmerBehaviour.TimeWithoutConsuming, agent.GetAgentVariable(_programmerBehaviour.TimeWithoutConsuming + Time.deltaTime));
        if (_smokeTime <= 0)
        {
            Debug.Log("Programador ha terminado de fumar");
            agent.SetAgentVariable(_programmerBehaviour.Stress, Random.Range(0, 0.2f));
            finished = true;
        }
    }
}
