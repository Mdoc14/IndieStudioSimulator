using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProgrammingAction : ASimpleAction
{
    private float _programmingTime;
    public ProgrammingAction(IAgent agent) : base(agent) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Programador está programando...");
        _programmingTime = Random.Range(5, agent.GetAgentVariable("maxWorkTime"));
        agent.SetAgentVariable("maxWorkTime", Random.Range(15f, 30f));
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
        if (_programmingTime <= 0)
        {
            Debug.Log("Programador ha terminado de programar");
            finished = true;
        }
    }
}
