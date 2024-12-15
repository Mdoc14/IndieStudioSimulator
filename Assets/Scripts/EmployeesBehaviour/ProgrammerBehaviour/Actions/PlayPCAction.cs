using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPCAction : ASimpleAction
{
    private float _playTime;
    private EmployeeBehaviour _programmerBehaviour;
    public PlayPCAction(IAgent agent) : base(agent) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Programador está jugando...");
        _playTime = Random.Range(5, 15);
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
        _playTime -= Time.deltaTime;
        agent.SetAgentVariable(_programmerBehaviour.TimeWithoutBath, agent.GetAgentVariable(_programmerBehaviour.TimeWithoutBath + Time.deltaTime));
        agent.SetAgentVariable(_programmerBehaviour.TimeWithoutConsuming, agent.GetAgentVariable(_programmerBehaviour.TimeWithoutConsuming + Time.deltaTime));
        if (_playTime <= 0)
        {
            Debug.Log("Programador ha terminado de jugar");
            agent.SetAgentVariable(_programmerBehaviour.Boredom, Random.Range(0, 0.2f));
            agent.SetAgentVariable(_programmerBehaviour.Motivation, agent.GetAgentVariable(_programmerBehaviour.Motivation + Random.Range(0.2f, 0.5f)));
            finished = true;
        }
    }
}
