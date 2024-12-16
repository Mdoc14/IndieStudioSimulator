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
        _playTime = Random.Range(10,25);
        _programmerBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        agent.SetBark("PlayPC");
        agent.SetAnimation("PlayPC");
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

        if (agent.GetAgentVariable(_programmerBehaviour.Motivation) <= 100f)
        {
            agent.SetAgentVariable(_programmerBehaviour.Motivation, agent.GetAgentVariable(_programmerBehaviour.Motivation) + Time.deltaTime);
        }
        else { agent.SetAgentVariable(_programmerBehaviour.Motivation, 100f); }

        if (_playTime <= 0)
        {
            agent.SetAgentVariable(_programmerBehaviour.Boredom, Random.Range(0,10));
            Debug.Log("Programador ha terminado de jugar");
            finished = true;
        }
    }
}
