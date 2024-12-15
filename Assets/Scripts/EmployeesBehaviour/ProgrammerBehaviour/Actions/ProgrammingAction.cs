using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProgrammingAction : ASimpleAction
{
    private float _programmingTime;
    private EmployeeBehaviour _programmerBehaviour;
    public ProgrammingAction(IAgent agent) : base(agent) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Programador está programando...");
        _programmerBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        _programmingTime = Random.Range(5, 15);
        agent.SetBark("Program");
        agent.SetAnimation("Program");
    }
    public override void Exit()
    {
        Debug.Log("Programador ha llegado al escritorio");
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        _programmingTime -= Time.deltaTime;
        agent.SetAgentVariable(_programmerBehaviour.TimeWithoutBath, agent.GetAgentVariable(_programmerBehaviour.TimeWithoutBath + Time.deltaTime));
        agent.SetAgentVariable(_programmerBehaviour.TimeWithoutConsuming, agent.GetAgentVariable(_programmerBehaviour.TimeWithoutConsuming + Time.deltaTime));
        if (_programmingTime <= 0)
        {
            Debug.Log("Programador ha terminado de programar");
            agent.SetAgentVariable(_programmerBehaviour.Stress, agent.GetAgentVariable(_programmerBehaviour.Stress + Random.Range(0,0.2f)));
            agent.SetAgentVariable(_programmerBehaviour.Boredom, agent.GetAgentVariable(_programmerBehaviour.Boredom + Random.Range(0,0.2f)));
            finished = true;
        }
    }
}
